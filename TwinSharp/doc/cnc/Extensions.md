# Extensions `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.CNC
  TwinSharp.CNC.Extensions[[Extensions]]
  end
```

## Members
### Methods
#### Public Static methods
| Returns | Name |
| --- | --- |
| `T` | [`ByteArrayToStructure`](#bytearraytostructure)(`byte``[]` bytes) |

## Details
### Methods
#### ByteArrayToStructure
[*Source code*](https://github.com///blob//TwinSharp/CNC/Extensions.cs#L8)
```csharp
public static T ByteArrayToStructure<T>(byte[] bytes)
where T : ValueType
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `byte``[]` | bytes |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
