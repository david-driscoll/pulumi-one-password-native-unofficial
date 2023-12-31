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
    'GetSocialSecurityNumberResult',
    'AwaitableGetSocialSecurityNumberResult',
    'get_social_security_number',
    'get_social_security_number_output',
]

@pulumi.output_type
class GetSocialSecurityNumberResult:
    def __init__(__self__, attachments=None, category=None, fields=None, id=None, name=None, notes=None, number=None, references=None, sections=None, tags=None, title=None, urls=None, vault=None):
        if attachments and not isinstance(attachments, dict):
            raise TypeError("Expected argument 'attachments' to be a dict")
        pulumi.set(__self__, "attachments", attachments)
        if category and not isinstance(category, dict):
            raise TypeError("Expected argument 'category' to be a dict")
        pulumi.set(__self__, "category", category)
        if fields and not isinstance(fields, dict):
            raise TypeError("Expected argument 'fields' to be a dict")
        pulumi.set(__self__, "fields", fields)
        if id and not isinstance(id, str):
            raise TypeError("Expected argument 'id' to be a str")
        pulumi.set(__self__, "id", id)
        if name and not isinstance(name, str):
            raise TypeError("Expected argument 'name' to be a str")
        pulumi.set(__self__, "name", name)
        if notes and not isinstance(notes, str):
            raise TypeError("Expected argument 'notes' to be a str")
        pulumi.set(__self__, "notes", notes)
        if number and not isinstance(number, str):
            raise TypeError("Expected argument 'number' to be a str")
        pulumi.set(__self__, "number", number)
        if references and not isinstance(references, list):
            raise TypeError("Expected argument 'references' to be a list")
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
        if urls and not isinstance(urls, list):
            raise TypeError("Expected argument 'urls' to be a list")
        pulumi.set(__self__, "urls", urls)
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
    @pulumi.getter
    def fields(self) -> Mapping[str, 'outputs.OutputField']:
        return pulumi.get(self, "fields")

    @property
    @pulumi.getter
    def id(self) -> str:
        """
        The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        """
        return pulumi.get(self, "id")

    @property
    @pulumi.getter
    def name(self) -> Optional[str]:
        return pulumi.get(self, "name")

    @property
    @pulumi.getter
    def notes(self) -> Optional[str]:
        return pulumi.get(self, "notes")

    @property
    @pulumi.getter
    def number(self) -> Optional[str]:
        return pulumi.get(self, "number")

    @property
    @pulumi.getter
    def references(self) -> Sequence['outputs.OutputReference']:
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
    def urls(self) -> Optional[Sequence['outputs.OutputUrl']]:
        return pulumi.get(self, "urls")

    @property
    @pulumi.getter
    def vault(self) -> 'outputs.OutputVault':
        return pulumi.get(self, "vault")


class AwaitableGetSocialSecurityNumberResult(GetSocialSecurityNumberResult):
    # pylint: disable=using-constant-test
    def __await__(self):
        if False:
            yield self
        return GetSocialSecurityNumberResult(
            attachments=self.attachments,
            category=self.category,
            fields=self.fields,
            id=self.id,
            name=self.name,
            notes=self.notes,
            number=self.number,
            references=self.references,
            sections=self.sections,
            tags=self.tags,
            title=self.title,
            urls=self.urls,
            vault=self.vault)


def get_social_security_number(id: Optional[str] = None,
                               title: Optional[str] = None,
                               vault: Optional[str] = None,
                               opts: Optional[pulumi.InvokeOptions] = None) -> AwaitableGetSocialSecurityNumberResult:
    """
    Use this data source to access information about an existing resource.

    :param str id: The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
    :param str title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
    :param str vault: The UUID of the vault the item is in.
    """
    __args__ = dict()
    __args__['id'] = id
    __args__['title'] = title
    __args__['vault'] = vault
    if opts is None:
        opts = pulumi.InvokeOptions()
    if opts.version is None:
        opts.version = _utilities.get_version()
        if opts.plugin_download_url is None:
            opts.plugin_download_url = _utilities.get_plugin_download_url()
    __ret__ = pulumi.runtime.invoke('one-password-native-unofficial:index:GetSocialSecurityNumber', __args__, opts=opts, typ=GetSocialSecurityNumberResult).value

    return AwaitableGetSocialSecurityNumberResult(
        attachments=__ret__.attachments,
        category=__ret__.category,
        fields=__ret__.fields,
        id=__ret__.id,
        name=__ret__.name,
        notes=__ret__.notes,
        number=__ret__.number,
        references=__ret__.references,
        sections=__ret__.sections,
        tags=__ret__.tags,
        title=__ret__.title,
        urls=__ret__.urls,
        vault=__ret__.vault)


@_utilities.lift_output_func(get_social_security_number)
def get_social_security_number_output(id: Optional[pulumi.Input[Optional[str]]] = None,
                                      title: Optional[pulumi.Input[Optional[str]]] = None,
                                      vault: Optional[pulumi.Input[str]] = None,
                                      opts: Optional[pulumi.InvokeOptions] = None) -> pulumi.Output[GetSocialSecurityNumberResult]:
    """
    Use this data source to access information about an existing resource.

    :param str id: The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
    :param str title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
    :param str vault: The UUID of the vault the item is in.
    """
    ...
