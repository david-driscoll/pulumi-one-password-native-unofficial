# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from . import _utilities
from . import outputs
from ._enums import *

__all__ = [
    'GetPassportResult',
    'AwaitableGetPassportResult',
    'get_passport',
    'get_passport_output',
]

@pulumi.output_type
class GetPassportResult:
    def __init__(__self__, attachments=None, category=None, date_of_birth=None, expiry_date=None, fields=None, full_name=None, gender=None, issued_on=None, issuing_authority=None, issuing_country=None, nationality=None, notes=None, number=None, place_of_birth=None, references=None, sections=None, tags=None, title=None, type=None, urls=None, uuid=None, vault=None):
        if attachments and not isinstance(attachments, dict):
            raise TypeError("Expected argument 'attachments' to be a dict")
        pulumi.set(__self__, "attachments", attachments)
        if category and not isinstance(category, dict):
            raise TypeError("Expected argument 'category' to be a dict")
        pulumi.set(__self__, "category", category)
        if date_of_birth and not isinstance(date_of_birth, str):
            raise TypeError("Expected argument 'date_of_birth' to be a str")
        pulumi.set(__self__, "date_of_birth", date_of_birth)
        if expiry_date and not isinstance(expiry_date, str):
            raise TypeError("Expected argument 'expiry_date' to be a str")
        pulumi.set(__self__, "expiry_date", expiry_date)
        if fields and not isinstance(fields, dict):
            raise TypeError("Expected argument 'fields' to be a dict")
        pulumi.set(__self__, "fields", fields)
        if full_name and not isinstance(full_name, str):
            raise TypeError("Expected argument 'full_name' to be a str")
        pulumi.set(__self__, "full_name", full_name)
        if gender and not isinstance(gender, str):
            raise TypeError("Expected argument 'gender' to be a str")
        pulumi.set(__self__, "gender", gender)
        if issued_on and not isinstance(issued_on, str):
            raise TypeError("Expected argument 'issued_on' to be a str")
        pulumi.set(__self__, "issued_on", issued_on)
        if issuing_authority and not isinstance(issuing_authority, str):
            raise TypeError("Expected argument 'issuing_authority' to be a str")
        pulumi.set(__self__, "issuing_authority", issuing_authority)
        if issuing_country and not isinstance(issuing_country, str):
            raise TypeError("Expected argument 'issuing_country' to be a str")
        pulumi.set(__self__, "issuing_country", issuing_country)
        if nationality and not isinstance(nationality, str):
            raise TypeError("Expected argument 'nationality' to be a str")
        pulumi.set(__self__, "nationality", nationality)
        if notes and not isinstance(notes, str):
            raise TypeError("Expected argument 'notes' to be a str")
        pulumi.set(__self__, "notes", notes)
        if number and not isinstance(number, str):
            raise TypeError("Expected argument 'number' to be a str")
        pulumi.set(__self__, "number", number)
        if place_of_birth and not isinstance(place_of_birth, str):
            raise TypeError("Expected argument 'place_of_birth' to be a str")
        pulumi.set(__self__, "place_of_birth", place_of_birth)
        if references and not isinstance(references, dict):
            raise TypeError("Expected argument 'references' to be a dict")
        pulumi.set(__self__, "references", references)
        if sections and not isinstance(sections, dict):
            raise TypeError("Expected argument 'sections' to be a dict")
        pulumi.set(__self__, "sections", sections)
        if tags and not isinstance(tags, list):
            raise TypeError("Expected argument 'tags' to be a list")
        pulumi.set(__self__, "tags", tags)
        if title and not isinstance(title, str):
            raise TypeError("Expected argument 'title' to be a str")
        pulumi.set(__self__, "title", title)
        if type and not isinstance(type, str):
            raise TypeError("Expected argument 'type' to be a str")
        pulumi.set(__self__, "type", type)
        if urls and not isinstance(urls, list):
            raise TypeError("Expected argument 'urls' to be a list")
        pulumi.set(__self__, "urls", urls)
        if uuid and not isinstance(uuid, str):
            raise TypeError("Expected argument 'uuid' to be a str")
        pulumi.set(__self__, "uuid", uuid)
        if vault and not isinstance(vault, dict):
            raise TypeError("Expected argument 'vault' to be a dict")
        pulumi.set(__self__, "vault", vault)

    @property
    @pulumi.getter
    def attachments(self) -> Mapping[str, 'outputs.OutputAttachment']:
        return pulumi.get(self, "attachments")

    @property
    @pulumi.getter
    def category(self) -> str:
        return pulumi.get(self, "category")

    @property
    @pulumi.getter(name="dateOfBirth")
    def date_of_birth(self) -> Optional[str]:
        return pulumi.get(self, "date_of_birth")

    @property
    @pulumi.getter(name="expiryDate")
    def expiry_date(self) -> Optional[str]:
        return pulumi.get(self, "expiry_date")

    @property
    @pulumi.getter
    def fields(self) -> Mapping[str, 'outputs.OutputField']:
        return pulumi.get(self, "fields")

    @property
    @pulumi.getter(name="fullName")
    def full_name(self) -> Optional[str]:
        return pulumi.get(self, "full_name")

    @property
    @pulumi.getter
    def gender(self) -> Optional[str]:
        return pulumi.get(self, "gender")

    @property
    @pulumi.getter(name="issuedOn")
    def issued_on(self) -> Optional[str]:
        return pulumi.get(self, "issued_on")

    @property
    @pulumi.getter(name="issuingAuthority")
    def issuing_authority(self) -> Optional[str]:
        return pulumi.get(self, "issuing_authority")

    @property
    @pulumi.getter(name="issuingCountry")
    def issuing_country(self) -> Optional[str]:
        return pulumi.get(self, "issuing_country")

    @property
    @pulumi.getter
    def nationality(self) -> Optional[str]:
        return pulumi.get(self, "nationality")

    @property
    @pulumi.getter
    def notes(self) -> Optional[str]:
        return pulumi.get(self, "notes")

    @property
    @pulumi.getter
    def number(self) -> Optional[str]:
        return pulumi.get(self, "number")

    @property
    @pulumi.getter(name="placeOfBirth")
    def place_of_birth(self) -> Optional[str]:
        return pulumi.get(self, "place_of_birth")

    @property
    @pulumi.getter
    def references(self) -> Mapping[str, 'outputs.OutputReference']:
        return pulumi.get(self, "references")

    @property
    @pulumi.getter
    def sections(self) -> Mapping[str, 'outputs.OutputSection']:
        return pulumi.get(self, "sections")

    @property
    @pulumi.getter
    def tags(self) -> Sequence[str]:
        """
        An array of strings of the tags assigned to the item.
        """
        return pulumi.get(self, "tags")

    @property
    @pulumi.getter
    def title(self) -> str:
        """
        The title of the item.
        """
        return pulumi.get(self, "title")

    @property
    @pulumi.getter
    def type(self) -> Optional[str]:
        return pulumi.get(self, "type")

    @property
    @pulumi.getter
    def urls(self) -> Optional[Sequence['outputs.OutputUrl']]:
        return pulumi.get(self, "urls")

    @property
    @pulumi.getter
    def uuid(self) -> str:
        """
        The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        """
        return pulumi.get(self, "uuid")

    @property
    @pulumi.getter
    def vault(self) -> Mapping[str, str]:
        return pulumi.get(self, "vault")


class AwaitableGetPassportResult(GetPassportResult):
    # pylint: disable=using-constant-test
    def __await__(self):
        if False:
            yield self
        return GetPassportResult(
            attachments=self.attachments,
            category=self.category,
            date_of_birth=self.date_of_birth,
            expiry_date=self.expiry_date,
            fields=self.fields,
            full_name=self.full_name,
            gender=self.gender,
            issued_on=self.issued_on,
            issuing_authority=self.issuing_authority,
            issuing_country=self.issuing_country,
            nationality=self.nationality,
            notes=self.notes,
            number=self.number,
            place_of_birth=self.place_of_birth,
            references=self.references,
            sections=self.sections,
            tags=self.tags,
            title=self.title,
            type=self.type,
            urls=self.urls,
            uuid=self.uuid,
            vault=self.vault)


def get_passport(title: Optional[str] = None,
                 uuid: Optional[str] = None,
                 vault: Optional[str] = None,
                 opts: Optional[pulumi.InvokeOptions] = None) -> AwaitableGetPassportResult:
    """
    Use this data source to access information about an existing resource.

    :param str title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
    :param str uuid: The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
    :param str vault: The UUID of the vault the item is in.
    """
    __args__ = dict()
    __args__['title'] = title
    __args__['uuid'] = uuid
    __args__['vault'] = vault
    if opts is None:
        opts = pulumi.InvokeOptions()
    if opts.version is None:
        opts.version = _utilities.get_version()
        if opts.plugin_download_url is None:
            opts.plugin_download_url = _utilities.get_plugin_download_url()
    __ret__ = pulumi.runtime.invoke('one-password-native-unoffical:index:GetPassport', __args__, opts=opts, typ=GetPassportResult).value

    return AwaitableGetPassportResult(
        attachments=__ret__.attachments,
        category=__ret__.category,
        date_of_birth=__ret__.date_of_birth,
        expiry_date=__ret__.expiry_date,
        fields=__ret__.fields,
        full_name=__ret__.full_name,
        gender=__ret__.gender,
        issued_on=__ret__.issued_on,
        issuing_authority=__ret__.issuing_authority,
        issuing_country=__ret__.issuing_country,
        nationality=__ret__.nationality,
        notes=__ret__.notes,
        number=__ret__.number,
        place_of_birth=__ret__.place_of_birth,
        references=__ret__.references,
        sections=__ret__.sections,
        tags=__ret__.tags,
        title=__ret__.title,
        type=__ret__.type,
        urls=__ret__.urls,
        uuid=__ret__.uuid,
        vault=__ret__.vault)


@_utilities.lift_output_func(get_passport)
def get_passport_output(title: Optional[pulumi.Input[Optional[str]]] = None,
                        uuid: Optional[pulumi.Input[Optional[str]]] = None,
                        vault: Optional[pulumi.Input[str]] = None,
                        opts: Optional[pulumi.InvokeOptions] = None) -> pulumi.Output[GetPassportResult]:
    """
    Use this data source to access information about an existing resource.

    :param str title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
    :param str uuid: The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
    :param str vault: The UUID of the vault the item is in.
    """
    ...
