# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from .. import _utilities

import types

__config__ = pulumi.Config('one-password-native-unofficial')


class _ExportableConfig(types.ModuleType):
    @property
    def connect_host(self) -> Optional[str]:
        return __config__.get('connectHost')

    @property
    def connect_token(self) -> Optional[str]:
        return __config__.get('connectToken')

    @property
    def service_account_token(self) -> Optional[str]:
        return __config__.get('serviceAccountToken')

