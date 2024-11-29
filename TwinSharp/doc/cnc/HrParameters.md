# HrParameters `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.CNC
  TwinSharp.CNC.HrParameters[[HrParameters]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `void` | [`EnableControlElement`](#enablecontrolelement)(`bool` enabled) |
| `void` | [`SignalCommandSemaphor`](#signalcommandsemaphor)(`bool` signal) |
| `void` | [`WriteCommandElement`](#writecommandelement)([`HLI_HB_HR_PARAMETER`](./HLI_HB_HR_PARAMETER.md) data) |

## Details
### Constructors
#### HrParameters
[*Source code*](https://github.com///blob//TwinSharp/CNC/ManualOperation.cs#L136)
```csharp
public HrParameters(AdsClient plcClient, int channelNumber)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | plcClient |   |
| `int` | channelNumber |   |

### Methods
#### EnableControlElement
[*Source code*](https://github.com///blob//TwinSharp/CNC/ManualOperation.cs#L143)
```csharp
public void EnableControlElement(bool enabled)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `bool` | enabled |   |

#### WriteCommandElement
[*Source code*](https://github.com///blob//TwinSharp/CNC/ManualOperation.cs#L150)
```csharp
public void WriteCommandElement(HLI_HB_HR_PARAMETER data)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`HLI_HB_HR_PARAMETER`](./HLI_HB_HR_PARAMETER.md) | data |   |

#### SignalCommandSemaphor
[*Source code*](https://github.com///blob//TwinSharp/CNC/ManualOperation.cs#L157)
```csharp
public void SignalCommandSemaphor(bool signal)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `bool` | signal |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
