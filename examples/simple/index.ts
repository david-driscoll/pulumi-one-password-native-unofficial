import * as op from "@pulumi/one-password-native";
import * as pulumi from '@pulumi/pulumi'

// const page = new op.StaticPage("page", {
//     indexContent: "<html><body><p>Hello world!</p></body></html>",
// });

// export const bucket = page.bucket;
// export const url = page.websiteUrl;
// new op.Item("test-item", {
//     vault: 'testing-pulumi',
//     fields: {
//         test: {
//             purpose: "PASSWORD",
//             value: '12345'
//         }
//     },
//     sections: {
//         mysection: {
//             fields: {
//                 test: {
//                     purpose: "NOTE",
//                     value: '12345'
//                 }
//             }

//         }
//     }
// }, {

// })
// new op.Item("test-item2", {
//     title: 'test-item2',
//     vault: 'testing-pulumi',
//     fields: {
//         Field: {
//             purpose: "PASSWORD",
//             value: '12345'
//         }
//     }
// }, {

// })

// new op.BankAccountItem('bank-account-info', {

//     vault: 'testing-pulumi',
//     accountNumber: "123456789",
//     branchInformation: {
//         address: "12345",
//         phone: "5698789987"
//     },
//     fields: {
//         "test": {
//             value: "abcd",
//             purpose: "NOTE"
//         }
//     },
//     sections: {
//         "my section": {
//             fields: {
//                 "my field": {
//                     value: "my value",
//                     purpose: "PASSWORD"
//                 }
//             }
//         }
//     }
// })

// const account = op.getBankAccountOutput({ vault: 'testing-pulumi', title: 'bank-account-info1a8ac7fc' })

// account.apply(z => {
//     if (z.accountNumber !== '123456789') {
//         throw new Error("wrong account found!!");
//     }
//     console.log(JSON.stringify(z));
// })

// new op.BankAccountItem('other-account-info', {

//     vault: 'testing-pulumi',
//     accountNumber: account.apply(z => Array.from(z.accountNumber ?? '').reverse().join(''))
// })

// op.Item.get('referenced-bank-account', 'bank-account-info1a8ac7fc', { vault: 'testing-pulumi' })

// const vault = op.getVaultOutput({ vault: 'testing-pulumi' });
// vault.apply(z => console.log(JSON.stringify(z)))

// const reference = op.getSecretReferenceOutput({ reference: "op://testing-pulumi/test note/password" });
// reference.apply(z => console.log(JSON.stringify(z)))



const login = new op.LoginItem('my-password', {
    vault: 'testing-pulumi',
    username: "me",
    attachments: {
        'my-attachment': new pulumi.asset.StringAsset("this is an attachment"),
        'package.json': new pulumi.asset.FileAsset("./package.json")
    },
    fields: {
        "password": {
            value: "secret1234",
            type: 'concealed'
        }
    },
    sections: {
        "mysection": {
            fields: {
                "password": {
                    value: "secret1235!",
                    type: 'concealed'
                }
            }
        }
    }
})

const member = new op.MembershipItem('random-membership', {
    vault: 'testing-pulumi',
    memberId: login.uuid,
    pin: "12345"
})

login.password.apply(z => {
    console.log('password:' + z)
})
login.fields.apply(z => {
    console.log('field password:' + z['password'].value)
})

// login.attachments.apply(z => z['my-attachment'].reference).apply(reference => op.getAttachment({ reference }))
login.getAttachment({ name: 'my-attachment' })
    .apply(z => {
        console.log(z.value)
        return Promise.resolve(z.value);
    })
