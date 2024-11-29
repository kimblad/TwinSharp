# TableState `Public class`

## Description
Represents the state of a table in a TwinCAT NC (Numerical Control) system.
            This class provides access to the 'User Counter'.
            It uses an AdsClient to communicate with the TwinCAT system and read the necessary data.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.NC
  TwinSharp.NC.TableState[[TableState]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `int` | [`UserCounter`](#usercounter)<br>'User Counter' (number of table user) | `get` |

## Details
### Summary
Represents the state of a table in a TwinCAT NC (Numerical Control) system.
            This class provides access to the 'User Counter'.
            It uses an AdsClient to communicate with the TwinCAT system and read the necessary data.

### Constructors
#### TableState
[*Source code*](https://github.com///blob//TwinSharp/NC/TableState.cs#L15)
```csharp
internal TableState(AdsClient client, uint id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | client |   |
| `uint` | id |   |

### Properties
#### UserCounter
```csharp
public int UserCounter { get; }
```
##### Summary
'User Counter' (number of table user)

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
