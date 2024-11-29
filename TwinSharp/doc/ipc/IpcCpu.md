# IpcCpu `Public class`

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
| `uint` | [`Frequency`](#frequency) | `get` |
| `short` | [`TemperatureCelsius`](#temperaturecelsius)<br>Requires BIOS API | `get` |
| `ushort` | [`UsagePercent`](#usagepercent) | `get` |

## Details
### Constructors
#### IpcCpu
[*Source code*](https://github.com///blob//TwinSharp/IPC/IpcCpu.cs#L12)
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

#### UsagePercent
```csharp
public ushort UsagePercent { get; }
```

#### TemperatureCelsius
```csharp
public short TemperatureCelsius { get; }
```
##### Summary
Requires BIOS API

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
