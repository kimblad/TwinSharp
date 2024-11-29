# ErrorManagement `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.CNC
  TwinSharp.CNC.ErrorManagement[[ErrorManagement]]
  end
```

## Details
### Constructors
#### ErrorManagement
[*Source code*](https://github.com///blob//TwinSharp/CNC/ErrorManagement.cs#L14)
```csharp
public ErrorManagement(AdsClient plcClient, int channelNumber)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | plcClient |   |
| `int` | channelNumber |   |

### Events
#### ErrorRecieved
```csharp
public event EventHandler<ErrorRecievedEventArgs> ErrorRecieved
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
