# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from .. import _utilities

__all__ = [
    'AdminConsoleSectionArgs',
    'HostingProviderSectionArgs',
]

@pulumi.input_type
class AdminConsoleSectionArgs:
    def __init__(__self__, *,
                 admin_console_url: Optional[pulumi.Input[str]] = None,
                 admin_console_username: Optional[pulumi.Input[str]] = None,
                 console_password: Optional[pulumi.Input[str]] = None):
        if admin_console_url is not None:
            pulumi.set(__self__, "admin_console_url", admin_console_url)
        if admin_console_username is not None:
            pulumi.set(__self__, "admin_console_username", admin_console_username)
        if console_password is not None:
            pulumi.set(__self__, "console_password", console_password)

    @property
    @pulumi.getter(name="adminConsoleUrl")
    def admin_console_url(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "admin_console_url")

    @admin_console_url.setter
    def admin_console_url(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "admin_console_url", value)

    @property
    @pulumi.getter(name="adminConsoleUsername")
    def admin_console_username(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "admin_console_username")

    @admin_console_username.setter
    def admin_console_username(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "admin_console_username", value)

    @property
    @pulumi.getter(name="consolePassword")
    def console_password(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "console_password")

    @console_password.setter
    def console_password(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "console_password", value)


@pulumi.input_type
class HostingProviderSectionArgs:
    def __init__(__self__, *,
                 name: Optional[pulumi.Input[str]] = None,
                 support_phone: Optional[pulumi.Input[str]] = None,
                 support_url: Optional[pulumi.Input[str]] = None,
                 website: Optional[pulumi.Input[str]] = None):
        if name is not None:
            pulumi.set(__self__, "name", name)
        if support_phone is not None:
            pulumi.set(__self__, "support_phone", support_phone)
        if support_url is not None:
            pulumi.set(__self__, "support_url", support_url)
        if website is not None:
            pulumi.set(__self__, "website", website)

    @property
    @pulumi.getter
    def name(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "name")

    @name.setter
    def name(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "name", value)

    @property
    @pulumi.getter(name="supportPhone")
    def support_phone(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "support_phone")

    @support_phone.setter
    def support_phone(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "support_phone", value)

    @property
    @pulumi.getter(name="supportUrl")
    def support_url(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "support_url")

    @support_url.setter
    def support_url(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "support_url", value)

    @property
    @pulumi.getter
    def website(self) -> Optional[pulumi.Input[str]]:
        return pulumi.get(self, "website")

    @website.setter
    def website(self, value: Optional[pulumi.Input[str]]):
        pulumi.set(self, "website", value)


