// Copyright 2016-2021, Pulumi Corporation.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

import * as pulumi from "@pulumi/pulumi";
import * as provider from "@pulumi/pulumi/provider";

// import { StaticPage, StaticPageArgs } from "./staticPage";

export class Provider implements provider.Provider {
    constructor(readonly version: string, readonly schema: string) { }

    /**
     * Construct creates a new component pulumi.
     *
     * @param name The name of the resource to create.
     * @param type The type of the resource to create.
     * @param inputs The inputs to the pulumi.
     * @param options the options for the pulumi.
     */
    async construct(name: string, type: string, inputs: pulumi.Inputs,
        options: pulumi.ComponentResourceOptions): Promise<provider.ConstructResult> {

        // TODO: Add support for additional component resources here.
        switch (type) {
            // case "onepassword:index:StaticPage":
            //     return await constructStaticPage(name, inputs, options);
            default:
                throw new Error(`unknown resource type ${type}`);
        }
    }

    // /**
    //  * Create allocates a new instance of the provided resource and returns its unique ID afterwards.
    //  * If this call fails, the resource must not have been created (i.e., it is "transactional").
    //  *
    //  * @param inputs The properties to set during creation.
    //  */
    // async create(urn: pulumi.URN, inputs: any): Promise<provider.CreateResult> { }

    // /**
    //  * Check validates that the given property bag is valid for a resource of the given type.
    //  *
    //  * @param olds The old input properties to use for validation.
    //  * @param news The new input properties to use for validation.
    //  */
    // async check(urn: pulumi.URN, olds: any, news: any): Promise<provider.CheckResult> {

    // }
    // /**
    //  * Diff checks what impacts a hypothetical update will have on the resource's properties.
    //  *
    //  * @param id The ID of the resource to diff.
    //  * @param olds The old values of properties to diff.
    //  * @param news The new values of properties to diff.
    //  */
    // async diff(id: pulumi.ID, urn: pulumi.URN, olds: any, news: any): Promise<provider.DiffResult> { }
    // /**
    //  * Reads the current live state associated with a pulumi.  Enough state must be included in the inputs to uniquely
    //  * identify the resource; this is typically just the resource ID, but it may also include some properties.
    //  */
    // async read(id: pulumi.ID, urn: pulumi.URN, props?: any): Promise<provider.ReadResult> { }
    // /**
    //  * Update updates an existing resource with new values.
    //  *
    //  * @param id The ID of the resource to update.
    //  * @param olds The old values of properties to update.
    //  * @param news The new values of properties to update.
    //  */
    // async update(id: pulumi.ID, urn: pulumi.URN, olds: any, news: any): Promise<provider.UpdateResult> { }
    // /**
    //  * Delete tears down an existing resource with the given ID.  If it fails, the resource is assumed to still exist.
    //  *
    //  * @param id The ID of the resource to delete.
    //  * @param props The current properties on the pulumi.
    //  */
    // async delete(id: pulumi.ID, urn: pulumi.URN, props: any): Promise<void> { }
    // /**
    //  * Call calls the indicated method.
    //  *
    //  * @param token The token of the method to call.
    //  * @param inputs The inputs to the method.
    //  */
    // async call(token: string, inputs: pulumi.Inputs): Promise<provider.InvokeResult> { }
    // /**
    //  * Invoke calls the indicated function.
    //  *
    //  * @param token The token of the function to call.
    //  * @param inputs The inputs to the function.
    //  */
    // async invoke(token: string, inputs: any): Promise<provider.InvokeResult> { }
}

// async function constructStaticPage(name: string, inputs: pulumi.Inputs,
//     options: pulumi.ComponentResourceOptions): Promise<provider.ConstructResult> {

//     // Create the component pulumi.
//     const staticPage = new StaticPage(name, inputs as StaticPageArgs, options);

//     // Return the component resource's URN and outputs as its state.
//     return {
//         urn: staticPage.urn,
//         state: {
//             bucket: staticPage.bucket,
//             websiteUrl: staticPage.websiteUrl,
//         },
//     };
// }
