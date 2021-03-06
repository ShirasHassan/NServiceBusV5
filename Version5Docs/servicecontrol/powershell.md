---
title: Managing ServiceControl via PowerShell
reviewed: 2021-08-23
---

## ServiceControl PowerShell

Before the graphical management utility existed to set up ServiceControl, PowerShell actions were used to add, remove, update and delete instances of ServiceControl.

## Prerequisites

The ServiceControlMgmt PowerShell module is called `ServiceControlMgmt` and is compatible with PowerShell 5. Versions of PowerShell later than 5 (including PowerShell Core) are not supported and might not work as expected.

NOTE: In order to run PowerShell cmdlets, the PowerShell execution policy needs to be set to `Unrestricted` or a bypass needs to be granted to the module file. Refer to the PowerShell documentation on how to change the execution policy.

## Loading and running the PowerShell module

The majority of the cmdlets will only work if the PowerShell session is running with administrator privileges. The ServiceControl installer creates a shortcut in the Windows start menu to launch an administrative PowerShell Session with the module automatically loaded. Alternatively, the module can be loaded directly into an an existing PowerShell session by loading `ServiceControlMgmt.psd1` using the `Import-Module` cmdlet as show below:

```ps
Import-Module "C:\Program Files (x86)\Particular Software\ServiceControl Management\ServiceControlMgmt\ServiceControlMgmt.psd1"
```

## Powershell Commands

For a complete overview of all cmdlets, visit the [Managing ServiceControl via PowerShell](/servicecontrol/installation-powershell.md) page.

## Troubleshooting via PowerShell

The ServiceControl Management PowerShell module offers some cmdlets to assist with troubleshooting the installation of ServiceControl instances.

### Check if a port is already in use

Before adding an instance of ServiceControl test if the port to use is currently in use.

```ps
Test-IfPortIsAvailable -Port 33333
```

This example shows the available ports out of a range of ports

```ps
33330..33339 | Test-IfPortIsAvailable | ? Available
```


### Checking and manipulating UrlAcls

The Window HTTPServer API is used by underlying components in ServiceControl. This API uses a permissions system to limit what accounts can add a HTTP listener to a specific URI. The standard mechanism for viewing and manipulating these ports is with the [netsh.exe](https://technet.microsoft.com/en-us/library/Cc725882.aspx) command line tool.

For example `netsh.exe http show urlacl` will list all of the available UrlAcls. This output is detailed but not easy to query. The ServiceControl Management PowerShell provides simplified PowerShell equivalents for listing, adding, and removing UrlAcls and makes the output easier to query.

For example the following command lists all of the UrlAcls assigned to any URI for port 33333.

```ps
Get-UrlAcls | ? Port -eq 33333
```

In this example any UrlAcl on port 33335 is remove

```ps
Get-UrlAcls | ? Port -eq 33335 | Remove-UrlAcl
```

The following example shows how to add a UrlAcl for a ServiceControl service that should only respond to a specific DNS name. This would require an update of the ServiceControl configuration file as well. Refer to [setting a custom host name and port number](setting-custom-hostname.md)

```ps
Add-UrlAcl -Url http://servicecontrol.mycompany.com:33333/api/ -Users Users
```
