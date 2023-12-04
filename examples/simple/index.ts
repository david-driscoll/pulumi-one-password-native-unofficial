import * as op from "pulumi-one-password-native-unoffical";
import * as pulumi from '@pulumi/pulumi'

// const login = new op.LoginItem('my-password', {
//     vault: 'testing-pulumi',
//     username: "me",
//     inputAttachments: {
//         'my-attachment': new pulumi.asset.StringAsset("this is an attachment"),
//         'package.json': new pulumi.asset.FileAsset("./package.json")
//     },
//     fields: {
//         "password": {
//             value: "secret1234",
//             type: 'concealed'
//         }
//     },
//     sections: {
//         "mysection": {
//             fields: {
//                 "password": {
//                     value: "secret1235!",
//                     type: 'concealed'
//                 }
//             }
//         }
//     }
// })


var ssn = new op.SocialSecurityNumberItem("my-ssn",
    {
        vault: "testing-pulumi",
        fields:
        {
            ["name"]: { type: op.FieldAssignmentType.Text, value: "thename" },
        },
        notes: "",
        title: "mytitles",
    });

// const member = new op.MembershipItem('random-membership', {
//     vault: 'testing-pulumi',
//     memberId: login.uuid,
//     pin: "12345"
// })

// login.password.apply(z => {
//     console.log('password:' + z)
// })
// login.fields.apply(z => {
//     console.log('field password:' + z['password'].value)
// })

// login.attachments.apply(z => z['my-attachment'].reference).apply(reference => op.getAttachment({ reference }))
//     //login.getAttachment({ name: 'my-attachment' })
//     .apply(z => {
//         console.log(z.value)
//         return Promise.resolve(z.value);
//     })
