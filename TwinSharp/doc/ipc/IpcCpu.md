# IpcCpu `Public class`

## Description
The IpcCpu class provides methods to interact with the CPU of a device via ADS (Automation Device Specification).
            It allows reading the CPU frequency, current CPU usage percentage, and current CPU temperature in Celsius.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.IPC
  TwinSharp.IPC.IpcCpu[[IpcCpu]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `uint` | [`Frequency`](#frequency)<br>CPU frequency (constant) | `get` |
| `short` | [`TemperatureCelsius`](#temperaturecelsius)<br>Current CPU Temperature (°C). Requires BIOS API. | `get` |
| `ushort` | [`UsagePercent`](#usagepercent)<br>Current CPU Usage (%) | `get` |

## Details
### Summary
The IpcCpu class provides methods to interact with the CPU of a device via ADS (Automation Device Specification).
            It allows reading the CPU frequency, current CPU usage percentage, and current CPU temperature in Celsius.

### Constructors
#### IpcCpu
[*Source code*](https://github.com///blob//TwinSharp/IPC/IpcCpu.cs#L16)
```csharp
internal IpcCpu(AdsClient client, ushort mdpId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | client |   |
| `ushort` | mdpId |   |

### Properties
#### Frequency
```csharp
public uint Frequency { get; }
```
##### Summary
CPU frequency (constant)

#### UsagePercent
```csharp
public ushort UsagePercent { get; }
```
##### Summary
Current CPU Usage (%)

#### TemperatureCelsius
```csharp
public short TemperatureCelsius { get; }
```
##### Summary
Current CPU Temperature (°C). Requires BIOS API.

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
