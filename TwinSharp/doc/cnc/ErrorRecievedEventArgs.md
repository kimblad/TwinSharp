# ErrorRecievedEventArgs `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.CNC
  TwinSharp.CNC.ErrorRecievedEventArgs[[ErrorRecievedEventArgs]]
  end
```

## Details
### Constructors
#### ErrorRecievedEventArgs
[*Source code*](https://github.com///blob//TwinSharp/CNC/ErrorManagement.cs#L83)
```csharp
internal ErrorRecievedEventArgs(HLI_ERROR_SATZ_KOPF error, string description)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`HLI_ERROR_SATZ_KOPF`](./HLI_ERROR_SATZ_KOPF.md) | error |   |
| `string` | description |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
