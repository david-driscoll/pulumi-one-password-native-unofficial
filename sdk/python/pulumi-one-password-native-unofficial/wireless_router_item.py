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
from ._inputs import *

__all__ = ['WirelessRouterItemArgs', 'WirelessRouterItem']

@pulumi.input_type
class WirelessRouterItemArgs:
    def __init__(__self__, *,
                 vault: pulumi.Input[str],
                 air_port_id: Optional[pulumi.Input[str]] = None,
                 attached_storage_password: Optional[pulumi.Input[str]] = None,
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 base_station_name: Optional[pulumi.Input[str]] = None,
                 base_station_password: Optional[pulumi.Input[str]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]] = None,
                 network_name: Optional[pulumi.Input[str]] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]] = None,
                 server_ip_address: Optional[pulumi.Input[str]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input['UrlArgs']]]] = None,
                 wireless_network_password: Optional[pulumi.Input[str]] = None,
                 wireless_security: Optional[pulumi.Input[str]] = None):
        """
        The set of arguments for constructing a WirelessRouterItem resource.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        :param pulumi.Input[str] category: The category of the vault the item is in.
        :param pulumi.Input[Sequence[pulumi.Input[str]]] tags: An array of strings of the tags assigned to the item.
        :param pulumi.Input[str] title: The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        """
        pulumi.set(__self__, "vault", vault)
        if air_port_id is not None:
            pulumi.set(__self__, "air_port_id", air_port_id)
        if attached_storage_password is not None:
            pulumi.set(__self__, "attached_storage_password", attached_storage_password)
        if attachments is not None:
            pulumi.set(__self__, "attachments", attachments)
        if base_station_name is not None:
            pulumi.set(__self__, "base_station_name", base_station_name)
        if base_station_password is not None:
            pulumi.set(__self__, "base_station_password", base_station_password)
        if category is not None:
            pulumi.set(__self__, "category", 'Wireless Router')
        if fields is not None:
            pulumi.set(__self__, "fields", fields)
        if network_name is not None:
            pulumi.set(__self__, "network_name", network_name)
        if notes is not None:
            pulumi.set(__self__, "notes", notes)
        if sections is not None:
            pulumi.set(__self__, "sections", sections)
        if server_ip_address is not None:
            pulumi.set(__self__, "server_ip_address", server_ip_address)
        if tags is not None:
            pulumi.set(__self__, "tags", tags)
        if title is not None:
            pulumi.set(__self__, "title", title)
        if urls is not None:
            pulumi.set(__self__, "urls", urls)
        if wireless_network_password is not None:
            pulumi.set(__self__, "wireless_network_password", wireless_network_password)
        if wireless_security is not None:
            pulumi.set(__self__, "wireless_security", wireless_security)

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
    @pulumi.getter(name="airPortId")
    def air_port_id(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "air_port_id")

    @air_port_id.setter
    def air_port_id(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "air_port_id", value)

    @property
    @pulumi.getter(name="attachedStoragePassword")
    def attached_storage_password(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "attached_storage_password")

    @attached_storage_password.setter
    def attached_storage_password(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "attached_storage_password", value)

    @property
    @pulumi.getter
    def attachments(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]]:
        return pulumi.get(self, "attachments")

    @attachments.setter
    def attachments(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]]):
        pulumi.set(self, "attachments", value)

    @property
    @pulumi.getter(name="baseStationName")
    def base_station_name(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "base_station_name")

    @base_station_name.setter
    def base_station_name(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "base_station_name", value)

    @property
    @pulumi.getter(name="baseStationPassword")
    def base_station_password(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "base_station_password")

    @base_station_password.setter
    def base_station_password(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "base_station_password", value)

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
    def fields(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]]:
        return pulumi.get(self, "fields")

    @fields.setter
    def fields(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input['FieldArgs']]]]):
        pulumi.set(self, "fields", value)

    @property
    @pulumi.getter(name="networkName")
    def network_name(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "network_name")

    @network_name.setter
    def network_name(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "network_name", value)

    @property
    @pulumi.getter
    def notes(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "notes")

    @notes.setter
    def notes(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "notes", value)

    @property
    @pulumi.getter
    def sections(self) -> Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]]:
        return pulumi.get(self, "sections")

    @sections.setter
    def sections(self, value: Optional[pulumi.Input[Mapping[str, pulumi.Input['SectionArgs']]]]):
        pulumi.set(self, "sections", value)

    @property
    @pulumi.getter(name="serverIpAddress")
    def server_ip_address(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "server_ip_address")

    @server_ip_address.setter
    def server_ip_address(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "server_ip_address", value)

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

    @property
    @pulumi.getter(name="wirelessNetworkPassword")
    def wireless_network_password(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "wireless_network_password")

    @wireless_network_password.setter
    def wireless_network_password(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "wireless_network_password", value)

    @property
    @pulumi.getter(name="wirelessSecurity")
    def wireless_security(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "wireless_security")

    @wireless_security.setter
    def wireless_security(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "wireless_security", value)


@pulumi.input_type
class _WirelessRouterItemState:
    def __init__(__self__, *,
                 vault: pulumi.Input[str]):
        """
        Input properties used for looking up and filtering WirelessRouterItem resources.
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


class WirelessRouterItem(pulumi.CustomResource):
    @overload
    def __init__(__self__,
                 resource_name: str,
                 opts: Optional[pulumi.ResourceOptions] = None,
                 air_port_id: Optional[pulumi.Input[str]] = None,
                 attached_storage_password: Optional[pulumi.Input[str]] = None,
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 base_station_name: Optional[pulumi.Input[str]] = None,
                 base_station_password: Optional[pulumi.Input[str]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['FieldArgs']]]]] = None,
                 network_name: Optional[pulumi.Input[str]] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['SectionArgs']]]]] = None,
                 server_ip_address: Optional[pulumi.Input[str]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['UrlArgs']]]]] = None,
                 vault: Optional[pulumi.Input[str]] = None,
                 wireless_network_password: Optional[pulumi.Input[str]] = None,
                 wireless_security: Optional[pulumi.Input[str]] = None,
                 __props__=None):
        """
        Create a WirelessRouterItem resource with the given unique name, props, and options.
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
                 args: WirelessRouterItemArgs,
                 opts: Optional[pulumi.ResourceOptions] = None):
        """
        Create a WirelessRouterItem resource with the given unique name, props, and options.
        :param str resource_name: The name of the resource.
        :param WirelessRouterItemArgs args: The arguments to use to populate this resource's properties.
        :param pulumi.ResourceOptions opts: Options for the resource.
        """
        ...
    def __init__(__self__, resource_name: str, *args, **kwargs):
        resource_args, opts = _utilities.get_resource_args_opts(WirelessRouterItemArgs, pulumi.ResourceOptions, *args, **kwargs)
        if resource_args is not None:
            __self__._internal_init(resource_name, opts, **resource_args.__dict__)
        else:
            __self__._internal_init(resource_name, *args, **kwargs)

    def _internal_init(__self__,
                 resource_name: str,
                 opts: Optional[pulumi.ResourceOptions] = None,
                 air_port_id: Optional[pulumi.Input[str]] = None,
                 attached_storage_password: Optional[pulumi.Input[str]] = None,
                 attachments: Optional[pulumi.Input[Mapping[str, pulumi.Input[Union[pulumi.Asset, pulumi.Archive]]]]] = None,
                 base_station_name: Optional[pulumi.Input[str]] = None,
                 base_station_password: Optional[pulumi.Input[str]] = None,
                 category: Optional[pulumi.Input[str]] = None,
                 fields: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['FieldArgs']]]]] = None,
                 network_name: Optional[pulumi.Input[str]] = None,
                 notes: Optional[pulumi.Input[str]] = None,
                 sections: Optional[pulumi.Input[Mapping[str, pulumi.Input[pulumi.InputType['SectionArgs']]]]] = None,
                 server_ip_address: Optional[pulumi.Input[str]] = None,
                 tags: Optional[pulumi.Input[Sequence[pulumi.Input[str]]]] = None,
                 title: Optional[pulumi.Input[str]] = None,
                 urls: Optional[pulumi.Input[Sequence[pulumi.Input[pulumi.InputType['UrlArgs']]]]] = None,
                 vault: Optional[pulumi.Input[str]] = None,
                 wireless_network_password: Optional[pulumi.Input[str]] = None,
                 wireless_security: Optional[pulumi.Input[str]] = None,
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
            __props__ = WirelessRouterItemArgs.__new__(WirelessRouterItemArgs)

            __props__.__dict__["air_port_id"] = air_port_id
            __props__.__dict__["attached_storage_password"] = None if attached_storage_password is None else pulumi.Output.secret(attached_storage_password)
            __props__.__dict__["attachments"] = attachments
            __props__.__dict__["base_station_name"] = base_station_name
            __props__.__dict__["base_station_password"] = None if base_station_password is None else pulumi.Output.secret(base_station_password)
            __props__.__dict__["category"] = 'Wireless Router'
            __props__.__dict__["fields"] = fields
            __props__.__dict__["network_name"] = network_name
            __props__.__dict__["notes"] = notes
            __props__.__dict__["sections"] = sections
            __props__.__dict__["server_ip_address"] = server_ip_address
            __props__.__dict__["tags"] = tags
            __props__.__dict__["title"] = title
            __props__.__dict__["urls"] = urls
            if vault is None and not opts.urn:
                raise TypeError("Missing required property 'vault'")
            __props__.__dict__["vault"] = vault
            __props__.__dict__["wireless_network_password"] = None if wireless_network_password is None else pulumi.Output.secret(wireless_network_password)
            __props__.__dict__["wireless_security"] = wireless_security
            __props__.__dict__["id"] = None
            __props__.__dict__["references"] = None
        secret_opts = pulumi.ResourceOptions(additional_secret_outputs=["attachedStoragePassword", "attachments", "baseStationPassword", "fields", "sections", "wirelessNetworkPassword"])
        opts = pulumi.ResourceOptions.merge(opts, secret_opts)
        super(WirelessRouterItem, __self__).__init__(
            'one-password-native-unofficial:index:WirelessRouterItem',
            resource_name,
            __props__,
            opts)

    @staticmethod
    def get(resource_name: str,
            id: pulumi.Input[str],
            opts: Optional[pulumi.ResourceOptions] = None,
            vault: Optional[pulumi.Input[str]] = None) -> 'WirelessRouterItem':
        """
        Get an existing WirelessRouterItem resource's state with the given name, id, and optional extra
        properties used to qualify the lookup.

        :param str resource_name: The unique name of the resulting resource.
        :param pulumi.Input[str] id: The unique provider ID of the resource to lookup.
        :param pulumi.ResourceOptions opts: Options for the resource.
        :param pulumi.Input[str] vault: The UUID of the vault the item is in.
        """
        opts = pulumi.ResourceOptions.merge(opts, pulumi.ResourceOptions(id=id))

        __props__ = _WirelessRouterItemState.__new__(_WirelessRouterItemState)

        __props__.__dict__["vault"] = vault
        __props__.__dict__["air_port_id"] = None
        __props__.__dict__["attached_storage_password"] = None
        __props__.__dict__["attachments"] = None
        __props__.__dict__["base_station_name"] = None
        __props__.__dict__["base_station_password"] = None
        __props__.__dict__["category"] = None
        __props__.__dict__["fields"] = None
        __props__.__dict__["id"] = None
        __props__.__dict__["network_name"] = None
        __props__.__dict__["notes"] = None
        __props__.__dict__["references"] = None
        __props__.__dict__["sections"] = None
        __props__.__dict__["server_ip_address"] = None
        __props__.__dict__["tags"] = None
        __props__.__dict__["title"] = None
        __props__.__dict__["urls"] = None
        __props__.__dict__["wireless_network_password"] = None
        __props__.__dict__["wireless_security"] = None
        return WirelessRouterItem(resource_name, opts=opts, __props__=__props__)

    @property
    @pulumi.getter(name="airPortId")
    def air_port_id(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "air_port_id")

    @property
    @pulumi.getter(name="attachedStoragePassword")
    def attached_storage_password(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "attached_storage_password")

    @property
    @pulumi.getter
    def attachments(self) -> pulumi.Output[Mapping[str, 'outputs.OutputAttachment']]:
        return pulumi.get(self, "attachments")

    @property
    @pulumi.getter(name="baseStationName")
    def base_station_name(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "base_station_name")

    @property
    @pulumi.getter(name="baseStationPassword")
    def base_station_password(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "base_station_password")

    @property
    @pulumi.getter
    def category(self) -> pulumi.Output[str]:
        return pulumi.get(self, "category")

    @property
    @pulumi.getter
    def fields(self) -> pulumi.Output[Mapping[str, 'outputs.OutputField']]:
        return pulumi.get(self, "fields")

    @property
    @pulumi.getter
    def id(self) -> pulumi.Output[str]:
        """
        The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        """
        return pulumi.get(self, "id")

    @property
    @pulumi.getter(name="networkName")
    def network_name(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "network_name")

    @property
    @pulumi.getter
    def notes(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "notes")

    @property
    @pulumi.getter
    def references(self) -> pulumi.Output[Sequence['outputs.OutputReference']]:
        return pulumi.get(self, "references")

    @property
    @pulumi.getter
    def sections(self) -> pulumi.Output[Mapping[str, 'outputs.OutputSection']]:
        return pulumi.get(self, "sections")

    @property
    @pulumi.getter(name="serverIpAddress")
    def server_ip_address(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "server_ip_address")

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

    @property
    @pulumi.getter(name="wirelessNetworkPassword")
    def wireless_network_password(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "wireless_network_password")

    @property
    @pulumi.getter(name="wirelessSecurity")
    def wireless_security(self) -> pulumi.Output[Optional[str]]:
        return pulumi.get(self, "wireless_security")

