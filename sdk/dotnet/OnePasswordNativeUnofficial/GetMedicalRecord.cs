// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetMedicalRecord
    {
        public static Task<GetMedicalRecordResult> InvokeAsync(GetMedicalRecordArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetMedicalRecordResult>("one-password-native-unofficial:index:GetMedicalRecord", args ?? new GetMedicalRecordArgs(), options.WithDefaults());

        public static Output<GetMedicalRecordResult> Invoke(GetMedicalRecordInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetMedicalRecordResult>("one-password-native-unofficial:index:GetMedicalRecord", args ?? new GetMedicalRecordInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetMedicalRecordArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("id")]
        public string? Id { get; set; }

        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault", required: true)]
        public string Vault { get; set; } = null!;

        public GetMedicalRecordArgs()
        {
        }
    }

    public sealed class GetMedicalRecordInvokeArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("id")]
        public Input<string>? Id { get; set; }

        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public Input<string>? Title { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault", required: true)]
        public Input<string> Vault { get; set; } = null!;

        public GetMedicalRecordInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetMedicalRecordResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly string? Date;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly string? HealthcareProfessional;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly string? Location;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.MedicalRecord.Outputs.MedicationSection? Medication;
        public readonly string? Notes;
        public readonly string? Patient;
        public readonly string? ReasonForVisit;
        public readonly ImmutableArray<Outputs.OutputReference> References;
        public readonly ImmutableDictionary<string, Outputs.OutputSection> Sections;
        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public readonly ImmutableArray<string> Tags;
        /// <summary>
        /// The title of the item.
        /// </summary>
        public readonly string Title;
        public readonly ImmutableArray<Outputs.OutputUrl> Urls;
        public readonly Outputs.OutputVault Vault;

        [OutputConstructor]
        private GetMedicalRecordResult(
            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            string? date,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string? healthcareProfessional,

            string id,

            string? location,

            Rocket.Surgery.OnePasswordNativeUnofficial.MedicalRecord.Outputs.MedicationSection? medication,

            string? notes,

            string? patient,

            string? reasonForVisit,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            Outputs.OutputVault vault)
        {
            Attachments = attachments;
            Category = category;
            Date = date;
            Fields = fields;
            HealthcareProfessional = healthcareProfessional;
            Id = id;
            Location = location;
            Medication = medication;
            Notes = notes;
            Patient = patient;
            ReasonForVisit = reasonForVisit;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Urls = urls;
            Vault = vault;
        }
    }
}
