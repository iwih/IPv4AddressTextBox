# IPv4AddressTextBox
IPv4 Address TextBox for .NET Framework, nothing more!
I tried to cover all transitions and character validation, please, if there is any other thing to be covered, let me know.

[![NuGet](https://img.shields.io/nuget/v/IPv4AddressTextBox.svg)](https://www.nuget.org/packages/IPv4AddressTextBox)
[![GitHub release](https://img.shields.io/github/release/iwih/IPv4AddressTextBox.svg?label=GitHub%20release)](https://github.com/iwih/IPv4AddressTextBox/releases)

# Implementation
You do not need any thing special, just add the assembly (dll) to your project's references, and you should find the control in the Toolbox of Visual Studio. 

# Usage

## Change control size
Use the Font property to change the size of the control.

## Programatically enter IP
### Set all from System.Net.IPAddress
IPv4AddressTextBox.IPAddress = new IPAddress("192.168.1.100");

### Set Octet individually 
IPv4AddressTextBox[0] = 192;
IPv4AddressTextBox[1] = 168;
IPv4AddressTextBox[2] = 1;
IPv4AddressTextBox[3] = 100;

### Set all from String
IPv4AddressTextBox.Text = "192.168.1.100";

# Compatibility
.NET Framework >= 2.0
