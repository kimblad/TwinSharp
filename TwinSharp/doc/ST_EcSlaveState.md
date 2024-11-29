# ST_EcSlaveState `Public class`

## Description
The structure ST_EcSlaveState contains the EtherCAT state and the link state of an EtherCAT slave device.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp
  TwinSharp.ST_EcSlaveState[[ST_EcSlaveState]]
  end
```

## Details
### Summary
The structure ST_EcSlaveState contains the EtherCAT state and the link state of an EtherCAT slave device.

### Constructors
#### ST_EcSlaveState
[*Source code*](https://github.com///blob//TwinSharp/Structs.cs#L121)
```csharp
public ST_EcSlaveState(byte[] bytes)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `byte``[]` | bytes |  |

##### Summary
Constructor for ST_EcSlaveState from a byte array of length 2.

##### Exceptions
| Name | Description |
| --- | --- |
| Exception |  |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
