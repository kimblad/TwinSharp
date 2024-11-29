# ST_TopologyDataEx `Public class`

## Description
The structure ST_TopologyDataEx contains information on EtherCAT topology and hot-connect groups.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp
  TwinSharp.ST_TopologyDataEx[[ST_TopologyDataEx]]
  end
```

## Details
### Summary
The structure ST_TopologyDataEx contains information on EtherCAT topology and hot-connect groups.

### Constructors
#### ST_TopologyDataEx
[*Source code*](https://github.com///blob//TwinSharp/Structs.cs#L314)
```csharp
public ST_TopologyDataEx(byte[] bytes)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `byte``[]` | bytes |  |

##### Summary
Constructor for ST_TopologyDataEx from a byte array of length 64.

##### Exceptions
| Name | Description |
| --- | --- |
| Exception |  |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
