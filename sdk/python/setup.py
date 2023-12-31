# coding=utf-8
# *** WARNING: this file was generated by Pulumi SDK Generator. ***
# *** Do not edit by hand unless you're certain you know what you are doing! ***

import errno
from setuptools import setup, find_packages
from setuptools.command.install import install
from subprocess import check_call


VERSION = "0.0.0"
PLUGIN_VERSION = "0.0.0"

class InstallPluginCommand(install):
    def run(self):
        install.run(self)
        try:
            check_call(['pulumi', 'plugin', 'install', 'resource', 'one-password-native-unofficial', PLUGIN_VERSION, '--server', 'github://api.github.com/david-driscoll'])
        except OSError as error:
            if error.errno == errno.ENOENT:
                print(f"""
                There was an error installing the one-password-native-unofficial resource provider plugin.
                It looks like `pulumi` is not installed on your system.
                Please visit https://pulumi.com/ to install the Pulumi CLI.
                You may try manually installing the plugin by running
                `pulumi plugin install resource one-password-native-unofficial {PLUGIN_VERSION}`
                """)
            else:
                raise


def readme():
    try:
        with open('README.md', encoding='utf-8') as f:
            return f.read()
    except FileNotFoundError:
        return "one-password-native-unofficial Pulumi Package - Development Version"


setup(name='pulumi-one-password-native-unofficial',
      version=VERSION,
      long_description=readme(),
      long_description_content_type='text/markdown',
      cmdclass={
          'install': InstallPluginCommand,
      },
      project_urls={
          'Repository': 'https://github.com/david-driscoll/pulumi-onepassword'
      },
      packages=find_packages(),
      package_data={
          'pulumi-one-password-native-unofficial': [
              'py.typed',
              'pulumi-plugin.json',
          ]
      },
      install_requires=[
          'parver>=0.2.1',
          'pulumi>=3.0.0,<4.0.0',
          'semver>=2.8.1'
      ],
      zip_safe=False)
