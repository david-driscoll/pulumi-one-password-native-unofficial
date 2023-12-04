# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from . import _utilities
from . import bankaccount as _bankaccount
from . import outputs
from ._enums import *

__all__ = [
    'GetBankAccountResult',
    'AwaitableGetBankAccountResult',
    'get_bank_account',
    'get_bank_account_output',
]

@pulumi.output_type
class GetBankAccountResult:
    def __init__(__self__, account_number=None, attachments=None, bank_name=None, branch_information=None, category=None, fields=None, iban=None, name_on_account=None, notes=None, pin=None, references=None, routing_number=None, sections=None, swift=None, tags=None, title=None, type=None, urls=None, uuid=None, vault=None):
        if account_number and not isinstance(account_number, str):
            raise TypeError("Expected argument 'account_number' to be a str")
        pulumi.set(__self__, "account_number", account_number)
        if attachments and not isinstance(attachments, dict):
            raise TypeError("Expected argument 'attachments' to be a dict")
        pulumi.set(__self__, "attachments", attachments)
        if bank_name and not isinstance(bank_name, str):
            raise TypeError("Expected argument 'bank_name' to be a str")
        pulumi.set(__self__, "bank_name", bank_name)
        if branch_information and not isinstance(branch_information, dict):
            raise TypeError("Expected argument 'branch_information' to be a dict")
        pulumi.set(__self__, "branch_information", branch_information)
        if category and not isinstance(category, dict):
            raise TypeError("Expected argument 'category' to be a dict")
        pulumi.set(__self__, "category", category)
        if fields and not isinstance(fields, dict):
            raise TypeError("Expected argument 'fields' to be a dict")
        pulumi.set(__self__, "fields", fields)
        if iban and not isinstance(iban, str):
            raise TypeError("Expected argument 'iban' to be a str")
        pulumi.set(__self__, "iban", iban)
        if name_on_account and not isinstance(name_on_account, str):
            raise TypeError("Expected argument 'name_on_account' to be a str")
        pulumi.set(__self__, "name_on_account", name_on_account)
        if notes and not isinstance(notes, str):
            raise TypeError("Expected argument 'notes' to be a str")
        pulumi.set(__self__, "notes", notes)
        if pin and not isinstance(pin, str):
            raise TypeError("Expected argument 'pin' to be a str")
        pulumi.set(__self__, "pin", pin)
        if references and not isinstance(references, dict):
            raise TypeError("Expected argument 'references' to be a dict")
        pulumi.set(__self__, "references", references)
        if routing_number and not isinstance(routing_number, str):
            raise TypeError("Expected argument 'routing_number' to be a str")
        pulumi.set(__self__, "routing_number", routing_number)
        if sections and not isinstance(sections, dict):
            raise TypeError("Expected argument 'sections' to be a dict")
        pulumi.set(__self__, "sections", sections)
        if swift and not isinstance(swift, str):
            raise TypeError("Expected argument 'swift' to be a str")
        pulumi.set(__self__, "swift", swift)
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
    @pulumi.getter(name="accountNumber")
    def account_number(self) -> Optional[str]:
        return pulumi.get(self, "account_number")

    @property
    @pulumi.getter
    def attachments(self) -> Mapping[str, 'outputs.OutputAttachment']:
        return pulumi.get(self, "attachments")

    @property
    @pulumi.getter(name="bankName")
    def bank_name(self) -> Optional[str]:
        return pulumi.get(self, "bank_name")

    @property
    @pulumi.getter(name="branchInformation")
    def branch_information(self) -> Optional['_bankaccount.outputs.BranchInformationSection']:
        return pulumi.get(self, "branch_information")

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
    def iban(self) -> Optional[str]:
        return pulumi.get(self, "iban")

    @property
    @pulumi.getter(name="nameOnAccount")
    def name_on_account(self) -> Optional[str]:
        return pulumi.get(self, "name_on_account")

    @property
    @pulumi.getter
    def notes(self) -> Optional[str]:
        return pulumi.get(self, "notes")

    @property
    @pulumi.getter
    def pin(self) -> Optional[str]:
        return pulumi.get(self, "pin")

    @property
    @pulumi.getter
    def references(self) -> Mapping[str, 'outputs.OutputReference']:
        return pulumi.get(self, "references")

    @property
    @pulumi.getter(name="routingNumber")
    def routing_number(self) -> Optional[str]:
        return pulumi.get(self, "routing_number")

    @property
    @pulumi.getter
    def sections(self) -> Mapping[str, 'outputs.OutputSection']:
        return pulumi.get(self, "sections")

    @property
    @pulumi.getter
    def swift(self) -> Optional[str]:
        return pulumi.get(self, "swift")

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


class AwaitableGetBankAccountResult(GetBankAccountResult):
    # pylint: disable=using-constant-test
    def __await__(self):
        if False:
            yield self
        return GetBankAccountResult(
            account_number=self.account_number,
            attachments=self.attachments,
            bank_name=self.bank_name,
            branch_information=self.branch_information,
            category=self.category,
            fields=self.fields,
            iban=self.iban,
            name_on_account=self.name_on_account,
            notes=self.notes,
            pin=self.pin,
            references=self.references,
            routing_number=self.routing_number,
            sections=self.sections,
            swift=self.swift,
            tags=self.tags,
            title=self.title,
            type=self.type,
            urls=self.urls,
            uuid=self.uuid,
            vault=self.vault)


def get_bank_account(title: Optional[str] = None,
                     uuid: Optional[str] = None,
                     vault: Optional[str] = None,
                     opts: Optional[pulumi.InvokeOptions] = None) -> AwaitableGetBankAccountResult:
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
    __ret__ = pulumi.runtime.invoke('one-password-native-unoffical:index:GetBankAccount', __args__, opts=opts, typ=GetBankAccountResult).value

    return AwaitableGetBankAccountResult(
        account_number=__ret__.account_number,
        attachments=__ret__.attachments,
        bank_name=__ret__.bank_name,
        branch_information=__ret__.branch_information,
        category=__ret__.category,
        fields=__ret__.fields,
        iban=__ret__.iban,
        name_on_account=__ret__.name_on_account,
        notes=__ret__.notes,
        pin=__ret__.pin,
        references=__ret__.references,
        routing_number=__ret__.routing_number,
        sections=__ret__.sections,
        swift=__ret__.swift,
        tags=__ret__.tags,
        title=__ret__.title,
        type=__ret__.type,
        urls=__ret__.urls,
        uuid=__ret__.uuid,
        vault=__ret__.vault)


@_utilities.lift_output_func(get_bank_account)
def get_bank_account_output(title: Optional[pulumi.Input[Optional[str]]] = None,
                            uuid: Optional[pulumi.Input[Optional[str]]] = None,
                            vault: Optional[pulumi.Input[str]] = None,
                            opts: Optional[pulumi.InvokeOptions] = None) -> pulumi.Output[GetBankAccountResult]:
    """
    Use this data source to access information about an existing resource.

    :param str title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
    :param str uuid: The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
    :param str vault: The UUID of the vault the item is in.
    """
    ...
