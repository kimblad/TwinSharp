# ST_EcSlaveIdentity `Public class`

## Description
The structure ST_EcSlaveIdentity contains the EtherCAT identity data for an EtherCAT slave device.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp
  TwinSharp.ST_EcSlaveIdentity[[ST_EcSlaveIdentity]]
  end
```

## Details
### Summary
The structure ST_EcSlaveIdentity contains the EtherCAT identity data for an EtherCAT slave device.

### Constructors
#### ST_EcSlaveIdentity
[*Source code*](https://github.com///blob//TwinSharp/Structs.cs#L161)
```csharp
public ST_EcSlaveIdentity(byte[] bytes)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `byte``[]` | bytes |  |

##### Summary
Constructor for ST_EcSlaveIdentity from a byte array of length 16.

##### Exceptions
| Name | Description |
| --- | --- |
| Exception |  |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
