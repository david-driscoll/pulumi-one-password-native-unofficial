# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import warnings
import pulumi
import pulumi.runtime
from typing import Any, Mapping, Optional, Sequence, Union, overload
from .. import _utilities

__all__ = [
    'BranchInformationSection',
]

@pulumi.output_type
class BranchInformationSection(dict):
    def __init__(__self__, *,
                 address: Optional[str] = None,
                 phone: Optional[str] = None):
        if address is not None:
            pulumi.set(__self__, "address", address)
        if phone is not None:
            pulumi.set(__self__, "phone", phone)

    @property
    @pulumi.getter
    def address(self) -> Optional[str]:
        return pulumi.get(self, "address")

    @property
    @pulumi.getter
    def phone(self) -> Optional[str]:
        return pulumi.get(self, "phone")


