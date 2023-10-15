import * as op from "@pulumi/onepassword";

// const page = new op.StaticPage("page", {
//     indexContent: "<html><body><p>Hello world!</p></body></html>",
// });

// export const bucket = page.bucket;
// export const url = page.websiteUrl;
new op.Item("test-item", {
    vault: 'testing-pulumi',
    fields: {
        test: {
            purpose: "PASSWORD",
            value: '12345'
        }
    },
    sections: {
        mysection: {
            fields: {
                test: {
                    purpose: "NOTE",
                    value: '12345'
                }
            }

        }
    }
}, {

})
new op.Item("test-item2", {
    title: 'test-item2',
    vault: 'testing-pulumi',
    fields: {
        Field: {
            purpose: "PASSWORD",
            value: '12345'
        }
    }
}, {

})