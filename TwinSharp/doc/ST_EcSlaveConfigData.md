# ST_EcSlaveConfigData `Public class`

## Description
The structure ST_EcSlaveConfigData contains the EtherCAT configuration data for an EtherCAT slave device.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp
  TwinSharp.ST_EcSlaveConfigData[[ST_EcSlaveConfigData]]
  end
```

## Details
### Summary
The structure ST_EcSlaveConfigData contains the EtherCAT configuration data for an EtherCAT slave device.

### Constructors
#### ST_EcSlaveConfigData
[*Source code*](https://github.com///blob//TwinSharp/Structs.cs#L232)
```csharp
public ST_EcSlaveConfigData(byte[] bytes)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `byte``[]` | bytes |  |

##### Summary
Constructor for ST_EcSlaveConfigData from a byte array of length 80.

##### Exceptions
| Name | Description |
| --- | --- |
| Exception |  |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
