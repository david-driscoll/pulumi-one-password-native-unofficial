# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from . import _utilities
from . import medicalrecord as _medicalrecord
from . import outputs
from ._enums import *
from ._inputs import *

__all__ = ['MedicalRecordItemArgs', 'MedicalRecordItem']

@pulumi.input_type
class MedicalRecordItemArgs:
    def __init__(__self__, *,
                 vault: pulumi.Input[str],
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 date: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]] = None,
                 healthcare_professional: Optional[pulumi.Input[str]] = None,
                 location: Optional[pulumi.Input[str]] = None,
                 medication: Optional[pulumi.Input['_medicalrecord.MedicationSectionArgs']] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 patient: Optional[pulumi.Input[str]] = None,
                 reason_for_visit: Optional[pulumi.Input[str]] = None,
                 references: Optional[pulumi.Input[Sequence[pulumi.Input['ReferenceArgs']]]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input['UrlArgs']]]] = None):
        """
        The set of arguments for constructing a MedicalRecordItem resource.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        :param pulumi.Input[str] category: The category of the vault the item is in.
        :param pulumi.Input[Sequence[pulumi.Input[str]]] tags: An array of strings of the tags assigned to the item.
        :param pulumi.Input[str] title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        """
        pulumi.set(__self__, "vault", vault)
        if attachments is not None:
            pulumi.set(__self__, "attachments", attachments)
        if category is not None:
            pulumi.set(__self__, "category", 'Medical Record')
        if date is not None:
            pulumi.set(__self__, "date", date)
        if fields is not None:
            pulumi.set(__self__, "fields", fields)
        if healthcare_professional is not None:
            pulumi.set(__self__, "healthcare_professional", healthcare_professional)
        if location is not None:
            pulumi.set(__self__, "location", location)
        if medication is not None:
            pulumi.set(__self__, "medication", medication)
        if notes is not None:
            pulumi.set(__self__, "notes", notes)
        if patient is not None:
            pulumi.set(__self__, "patient", patient)
        if reason_for_visit is not None:
            pulumi.set(__self__, "reason_for_visit", reason_for_visit)
        if references is not None:
            pulumi.set(__self__, "references", references)
        if sections is not None:
            pulumi.set(__self__, "sections", sections)
        if tags is not None:
            pulumi.set(__self__, "tags", tags)
        if title is not None:
            pulumi.set(__self__, "title", title)
        if urls is not None:
            pulumi.set(__self__, "urls", urls)

    @property
    @pulumi.getter
    def vault(self) -> pulumi.Input[str]:
        """
        The UUID of the vault the item is in.
        """
        return pulumi.get(self, "vault")

    @vault.setter
    def vault(self, value: pulumi.Input[str]):
        pulumi.set(self, "vault", value)

    @property
    @pulumi.getter
    def attachments(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]]:
        return pulumi.get(self, "attachments")

    @attachments.setter
    def attachments(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]]):
        pulumi.set(self, "attachments", value)

    @property
    @pulumi.getter
    def category(self) -> Optional[pulumi.Input[str]]:
        """
        The category of the vault the item is in.
        """
        return pulumi.get(self, "category")

    @category.setter
    def category(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "category", value)

    @property
    @pulumi.getter
    def date(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "date")

    @date.setter
    def date(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "date", value)

    @property
    @pulumi.getter
    def fields(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]]:
        return pulumi.get(self, "fields")

    @fields.setter
    def fields(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]]):
        pulumi.set(self, "fields", value)

    @property
    @pulumi.getter(name="healthcareProfessional")
    def healthcare_professional(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "healthcare_professional")

    @healthcare_professional.setter
    def healthcare_professional(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "healthcare_professional", value)

    @property
    @pulumi.getter
    def location(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "location")

    @location.setter
    def location(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "location", value)

    @property
    @pulumi.getter
    def medication(self) -> Optional[pulumi.Input['_medicalrecord.MedicationSectionArgs']]:
        return pulumi.get(self, "medication")

    @medication.setter
    def medication(self, value: Optional[pulumi.Input['_medicalrecord.MedicationSectionArgs']]):
        pulumi.set(self, "medication", value)

    @property
    @pulumi.getter
    def notes(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "notes")

    @notes.setter
    def notes(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "notes", value)

    @property
    @pulumi.getter
    def patient(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "patient")

    @patient.setter
    def patient(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "patient", value)

    @property
    @pulumi.getter(name="reasonForVisit")
    def reason_for_visit(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "reason_for_visit")

    @reason_for_visit.setter
    def reason_for_visit(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "reason_for_visit", value)

    @property
    @pulumi.getter
    def references(self) -> Optional[pulumi.Input[Sequence[pulumi.Input['ReferenceArgs']]]]:
        return pulumi.get(self, "references")

    @references.setter
    def references(self, value: Optional[pulumi.Input[Sequence[pulumi.Input['ReferenceArgs']]]]):
        pulumi.set(self, "references", value)

    @property
    @pulumi.getter
    def sections(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]]:
        return pulumi.get(self, "sections")

    @sections.setter
    def sections(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]]):
        pulumi.set(self, "sections", value)

    @property
    @pulumi.getter
    def tags(self) -> Optional[pulumi.Input[Sequence[pulumi.Input[str]]]]:
        """
        An array of strings of the tags assigned to the item.
        """
        return pulumi.get(self, "tags")

    @tags.setter
    def tags(self, value: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]]):
        pulumi.set(self, "tags", value)

    @property
    @pulumi.getter
    def title(self) -> Optional[pulumi.Input[str]]:
        """
        The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        """
        return pulumi.get(self, "title")

    @title.setter
    def title(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "title", value)

    @property
    @pulumi.getter
    def urls(self) -> Optional[pulumi.Input[Sequence[pulumi.Input['UrlArgs']]]]:
        return pulumi.get(self, "urls")

    @urls.setter
    def urls(self, value: Optional[pulumi.Input[Sequence[pulumi.Input['UrlArgs']]]]):
        pulumi.set(self, "urls", value)


@pulumi.input_type
class _MedicalRecordItemState:
    def __init__(__self__, *,
                 vault: pulumi.Input[str]):
        """
        Input properties used for looking up and filtering MedicalRecordItem resources.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        """
        pulumi.set(__self__, "vault", vault)

    @property
    @pulumi.getter
    def vault(self) -> pulumi.Input[str]:
        """
        The UUID of the vault the item is in.
        """
        return pulumi.get(self, "vault")

    @vault.setter
    def vault(self, value: pulumi.Input[str]):
        pulumi.set(self, "vault", value)


class MedicalRecordItem(pulumi.CustomResource):
    @overload
    def __init__(__self__,
                 resource_name: str,
                 opts: Optional[pulumi.ResourceOptions] = None,
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 date: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['FieldArgs']]]]] = None,
                 healthcare_professional: Optional[pulumi.Input[str]] = None,
                 location: Optional[pulumi.Input[str]] = None,
                 medication: Optional[pulumi.Input[pulumi.InputType['_medicalrecord.MedicationSectionArgs']]] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 patient: Optional[pulumi.Input[str]] = None,
                 reason_for_visit: Optional[pulumi.Input[str]] = None,
                 references: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['ReferenceArgs']]]]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['SectionArgs']]]]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['UrlArgs']]]]] = None,
                 vault: Optional[pulumi.Input[str]] = None,
                 __props__=None):
        """
        Create a MedicalRecordItem resource with the given unique name, props, and options.
        :param str resource_name: The name of the resource.
        :param pulumi.ResourceOptions opts: Options for the resource.
        :param pulumi.Input[str] category: The category of the vault the item is in.
        :param pulumi.Input[Sequence[pulumi.Input[str]]] tags: An array of strings of the tags assigned to the item.
        :param pulumi.Input[str] title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        """
        ...
    @overload
    def __init__(__self__,
                 resource_name: str,
                 args: MedicalRecordItemArgs,
                 opts: Optional[pulumi.ResourceOptions] = None):
        """
        Create a MedicalRecordItem resource with the given unique name, props, and options.
        :param str resource_name: The name of the resource.
        :param MedicalRecordItemArgs args: The arguments to use to populate this resource's properties.
        :param pulumi.ResourceOptions opts: Options for the resource.
        """
        ...
    def __init__(__self__, resource_name: str, *args, **kwargs):
        resource_args, opts = _utilities.get_resource_args_opts(MedicalRecordItemArgs, pulumi.ResourceOptions, *args, **kwargs)
        if resource_args is not None:
            __self__._internal_init(resource_name, opts, **resource_args.__dict__)
        else:
            __self__._internal_init(resource_name, *args, **kwargs)

    def _internal_init(__self__,
                 resource_name: str,
                 opts: Optional[pulumi.ResourceOptions] = None,
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 date: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['FieldArgs']]]]] = None,
                 healthcare_professional: Optional[pulumi.Input[str]] = None,
                 location: Optional[pulumi.Input[str]] = None,
                 medication: Optional[pulumi.Input[pulumi.InputType['_medicalrecord.MedicationSectionArgs']]] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 patient: Optional[pulumi.Input[str]] = None,
                 reason_for_visit: Optional[pulumi.Input[str]] = None,
                 references: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['ReferenceArgs']]]]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['SectionArgs']]]]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['UrlArgs']]]]] = None,
                 vault: Optional[pulumi.Input[str]] = None,
                 __props__=None):
        if opts is None:
            opts = pulumi.ResourceOptions()
        if not isinstance(opts, pulumi.ResourceOptions):
            raise TypeError('Expected resource options to be a ResourceOptions instance')
        if opts.version is None:
            opts.version = _utilities.get_version()
        if opts.plugin_download_url is None:
            opts.plugin_download_url = _utilities.get_plugin_download_url()
        if opts.id is None:
            if __props__ is not None:
                raise TypeError('__props__ is only valid when passed in combination with a valid opts.id to get an existing resource')
            __props__ = MedicalRecordItemArgs.__new__(MedicalRecordItemArgs)

            __props__.__dict__["attachments"] = attachments
            __props__.__dict__["category"] = 'Medical Record'
            __props__.__dict__["date"] = date
            __props__.__dict__["fields"] = fields
            __props__.__dict__["healthcare_professional"] = healthcare_professional
            __props__.__dict__["location"] = location
            __props__.__dict__["medication"] = medication
            __props__.__dict__["notes"] = notes
            __props__.__dict__["patient"] = patient
            __props__.__dict__["reason_for_visit"] = reason_for_visit
            __props__.__dict__["references"] = references
            __props__.__dict__["sections"] = sections
            __props__.__dict__["tags"] = tags
            __props__.__dict__["title"] = title
            __props__.__dict__["urls"] = urls
            if vault is None and not opts.urn:
                raise TypeError("Missing required property 'vault'")
            __props__.__dict__["vault"] = vault
            __props__.__dict__["id"] = None
        secret_opts = pulumi.ResourceOptions(additional_secret_outputs=["attachments", "fields", "sections"])
        opts = pulumi.ResourceOptions.merge(opts, secret_opts)
        super(MedicalRecordItem, __self__).__init__(
            'one-password-native-unofficial:index:MedicalRecordItem',
            resource_name,
            __props__,
            opts)

    @staticmethod
    def get(resource_name: str,
            id: pulumi.Input[str],
            opts: Optional[pulumi.ResourceOptions] = None,
            vault: Optional[pulumi.Input[str]] = None) -> 'MedicalRecordItem':
        """
        Get an existing MedicalRecordItem resource's state with the given name, id, and optional extra
        properties used to qualify the lookup.

        :param str resource_name: The unique name of the resulting resource.
        :param pulumi.Input[str] id: The unique provider ID of the resource to lookup.
        :param pulumi.ResourceOptions opts: Options for the resource.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        """
        opts = pulumi.ResourceOptions.merge(opts, pulumi.ResourceOptions(id=id))

        __props__ = _MedicalRecordItemState.__new__(_MedicalRecordItemState)

        __props__.__dict__["vault"] = vault
        __props__.__dict__["attachments"] = None
        __props__.__dict__["category"] = None
        __props__.__dict__["date"] = None
        __props__.__dict__["fields"] = None
        __props__.__dict__["healthcare_professional"] = None
        __props__.__dict__["id"] = None
        __props__.__dict__["location"] = None
        __props__.__dict__["medication"] = None
        __props__.__dict__["notes"] = None
        __props__.__dict__["patient"] = None
        __props__.__dict__["reason_for_visit"] = None
        __props__.__dict__["references"] = None
        __props__.__dict__["sections"] = None
        __props__.__dict__["tags"] = None
        __props__.__dict__["title"] = None
        __props__.__dict__["urls"] = None
        return MedicalRecordItem(resource_name, opts=opts, __props__=__props__)

    @property
    @pulumi.getter
    def attachments(self) -> pulumi.Output[Mapping[str, 'outputs.OutputAttachment']]:
        return pulumi.get(self, "attachments")

    @property
    @pulumi.getter
    def category(self) -> pulumi.Output[str]:
        return pulumi.get(self, "category")

    @property
    @pulumi.getter
    def date(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "date")

    @property
    @pulumi.getter
    def fields(self) -> pulumi.Output[Mapping[str, 'outputs.OutputField']]:
        return pulumi.get(self, "fields")

    @property
    @pulumi.getter(name="healthcareProfessional")
    def healthcare_professional(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "healthcare_professional")

    @property
    @pulumi.getter
    def id(self) -> pulumi.Output[str]:
        """
        The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        """
        return pulumi.get(self, "id")

    @property
    @pulumi.getter
    def location(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "location")

    @property
    @pulumi.getter
    def medication(self) -> pulumi.Output[Optional['_medicalrecord.outputs.MedicationSection']]:
        return pulumi.get(self, "medication")

    @property
    @pulumi.getter
    def notes(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "notes")

    @property
    @pulumi.getter
    def patient(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "patient")

    @property
    @pulumi.getter(name="reasonForVisit")
    def reason_for_visit(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "reason_for_visit")

    @property
    @pulumi.getter
    def references(self) -> pulumi.Output[Sequence['outputs.OutputReference']]:
        return pulumi.get(self, "references")

    @property
    @pulumi.getter
    def sections(self) -> pulumi.Output[Mapping[str, 'outputs.OutputSection']]:
        return pulumi.get(self, "sections")

    @property
    @pulumi.getter
    def tags(self) -> pulumi.Output[Sequence[str]]:
        """
        An array of strings of the tags assigned to the item.
        """
        return pulumi.get(self, "tags")

    @property
    @pulumi.getter
    def title(self) -> pulumi.Output[str]:
        """
        The title of the item.
        """
        return pulumi.get(self, "title")

    @property
    @pulumi.getter
    def urls(self) -> pulumi.Output[Optional[Sequence['outputs.OutputUrl']]]:
        return pulumi.get(self, "urls")

    @property
    @pulumi.getter
    def vault(self) -> pulumi.Output[Mapping[str, str]]:
        return pulumi.get(self, "vault")

