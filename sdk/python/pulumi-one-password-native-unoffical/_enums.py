# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

from enum import Enum

__all__ = [
    'Category',
    'FieldAssignmentType',
    'FieldPurpose',
    'ResponseFieldType',
]


class Category(str, Enum):
    """
    The category of the item. One of [ApiCredential, BankAccount, CreditCard, CryptoWallet, Database, Document, DriverLicense, EmailAccount, Identity, Item, Login, MedicalRecord, Membership, OutdoorLicense, Passport, Password, RewardProgram, SshKey, SecureNote, Server, SocialSecurityNumber, SoftwareLicense, WirelessRouter]
    """
    API_CREDENTIAL = "API Credential"
    BANK_ACCOUNT = "Bank Account"
    CREDIT_CARD = "Credit Card"
    CRYPTO_WALLET = "Crypto Wallet"
    DATABASE = "Database"
    DOCUMENT = "Document"
    DRIVER_LICENSE = "Driver License"
    EMAIL_ACCOUNT = "Email Account"
    IDENTITY = "Identity"
    ITEM = "Item"
    LOGIN = "Login"
    MEDICAL_RECORD = "Medical Record"
    MEMBERSHIP = "Membership"
    OUTDOOR_LICENSE = "Outdoor License"
    PASSPORT = "Passport"
    PASSWORD = "Password"
    REWARD_PROGRAM = "Reward Program"
    SSH_KEY = "SSH Key"
    SECURE_NOTE = "Secure Note"
    SERVER = "Server"
    SOCIAL_SECURITY_NUMBER = "Social Security Number"
    SOFTWARE_LICENSE = "Software License"
    WIRELESS_ROUTER = "Wireless Router"


class FieldAssignmentType(str, Enum):
    CONCEALED = "concealed"
    TEXT = "text"
    EMAIL = "email"
    URL = "url"
    DATE = "date"
    MONTH_YEAR = "monthYear"
    PHONE = "phone"


class FieldPurpose(str, Enum):
    USERNAME = "USERNAME"
    PASSWORD = "PASSWORD"
    NOTE = "NOTE"


class ResponseFieldType(str, Enum):
    UNKNOWN = "UNKNOWN"
    ADDRESS = "ADDRESS"
    CONCEALED = "CONCEALED"
    CREDIT_CARD_NUMBER = "CREDIT_CARD_NUMBER"
    CREDIT_CARD_TYPE = "CREDIT_CARD_TYPE"
    DATE = "Date"
    EMAIL = "EMAIL"
    GENDER = "GENDER"
    MONTH_YEAR = "MONTH_YEAR"
    OTP = "OTP"
    PHONE = "PHONE"
    REFERENCE = "REFERENCE"
    STRING = "STRING"
    URL = "URL"
    FILE = "FILE"
    SSH_KEY = "SSHKEY"
