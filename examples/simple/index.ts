import * as op from "@pulumi/onepassword";

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

new op.BankAccountItem('bank-account-info', {

    vault: 'testing-pulumi',
    accountNumber: "123456789",
    branchInformation: {
        address: "12345",
        phone: "5698789987"
    },
    fields: {
        "test": {
            value: "abcd",
            purpose: "NOTE"
        }
    },
    sections: {
        "my section": {
            fields: {
                "my field": {
                    value: "my value",
                    purpose: "PASSWORD"
                }
            }
        }
    }
})