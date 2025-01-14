# TwinSharp
TwinSharp is a free open source library for .NET that makes it easy to represent and control TwinCAT objects in C#. It communicates with ADS to a TwinCAT XAR system running locally or on a remote target. 

TwinSharp connects to all TwinCAT 3 systems and does not require any special adaptation or PLC code running on the targeted system.

For example TwinSharp can be used to connect to a remote TwinCAT axis and read its status and settings. 
Here's a custom user control built using TwinSharp. It mimics the "Axis online" view from TwinCAT but does not require TwinCAT XAE to be installed.

![online-view](https://github.com/user-attachments/assets/88c7e236-9325-401d-b033-34c99c4f91be)


Here's a list of some major components that can be viewed, altered and controlled with TwinSharp. For a full list, please refer to the documentation or ask :)


## EtherCAT
 - List all EtherCAT masters that exists in a local or remote system.
 - Get information about all configured EtherCAT slaves.
 - Get information about all connected EtherCAT slaves.
 - Get information about the topology of the network.
 - Read abnormal state change count of slaves.
 - CoE read and write to masters and slaves.

To find all EtherCAT masters on a target system, first create a TcSystem and then ues the method ListEtherCatMasters().
```csharp
var target = AmsNetId.Local;
var tcSystem = new TcSystem(target);
var masters = tcSystem.ListEtherCatMasters();
```

## Realtime system
 - Read and set the number of shared/isolated cores.
 - Read the CPU usage.
 - Read the TwinCAT system CPU latency.

To create a TwinSharp representation of the realtime system, first create a TcSystem object. Then the real time object can be found there.
```csharp
var target = AmsNetId.Local;
var tcSystem = new TcSystem(target);
var realtime = tcSystem.Realtime;
```

## PLC
 - Start, stop and reset a PLC on the local or remote TwinCAT system.
 - Read the number of "online changes" in the PLC.
 - Read if Windows is in a BSOD.
 - Read if Windows on the TwinCAT system is shutting down.
 - Read the timestamp of when the PLC was compiled.
 - Control if outputs should be zeroed when a breakpoint is hit in the PLC.

Create a representation of a TwinCAT PLC like this:
```csharp
var plcClient = new AdsClient();
plcClient.Connect(AmsPort.PlcRuntime_851);
var plc = new PLC(plcClient);
```
   
## NC Motion
 - View all available axes on a TwinCAT system.
 - Command motion to axes.
 - Couple axes.
 - Change axis encoder scaling, on the fly.
 - Set current position.
 - Enable or disable axis monitoring functions.
 - View and change axis settings including those of the encoder and drive.
 - Control axis channels and axis groups.
 - Control, modify and delete NC tables.

Create an NC object that contains all axes, channels, groups and tables on a target like this:
```csharp
var target = AmsNetId.Local;
var nc = new NC(target);
```

## IPC
The IPC class describes a Beckhoff Industrial PC.
 - Read temperatures of CPU and main board.
 - Read the boot count and current uptime.
 - Read BIOS version.
 - Read fan speed RPM.
 - Control Network cards, IP adresses, DHCP etc.
 - Read status of connected UPS, such as battery capacity, power fail count, fan error etc.
 - Read version of installed TwinCAT version and operating system version.
 - Control screen brightness of connected Beckhoff screens.
 - Disable/enable the Beckhoff security wizard.
 - Control current time and timezone of the TwinCAT system.


To create a TwinSharp representation of a local IPC do like this:
```csharp
var target = AmsNetId.Local;
var ipc = new IPC(target);
```

## File system
 - Create, delete, rename, view or write files and folders on a TwinCAT system.
 - Search for files.
 - View file properties and attributes.


To create a file system object, first create a TcSystem object. Then the file system object can be found there.
```csharp
var target = AmsNetId.Local;
var tcSystem = new TcSystem(target);
var fileSystem = tcSystem.FileSystem;
```

## License system
 - Get a list of valid licenses that exists on a remote or local target.
 - Get a list of invalid licenses.


To get the License object which provides access to the TwinCAT system's license information, you first create a TcSystem with your desired AmsNetId. 
```csharp
var target = AmsNetId.Local; 
var tcSystem = new TcSystem(target);
var licenseSystem = tcSystem.License;
```

## Various
TwinSharp can list all AMS routes that exists on the local system. It is a static method in the TcSystem class.


```csharp
AmsRoute[] routes = TcSystem.ListLocalStaticRoutes();
```

# Installation
Clone/download this project and build with Visual Studio. Or get the NuGet package: https://www.nuget.org/packages/TwinSharp.Core

# Additional info
Development has been focused on TwinCAT 3 systems.

This project is not affiliated or endorsed with Beckhoff in any way. I'm simply a guy who love all stuff Beckhoff/EtherCAT and try to make it available to as many as possible.

Beckhoff®, TwinCAT®, TwinCAT/BSD®, TC/BSD®, EtherCAT®, EtherCAT G®, EtherCAT G10®, EtherCAT P®, Safety over EtherCAT®, TwinSAFE®, XFC®, XTS® and XPlanar® are registered trademarks of and licensed by Beckhoff Automation GmbH. 

## License
TwinSharp is provided under the permissive MIT license and is free to modify and use for any purpose.
