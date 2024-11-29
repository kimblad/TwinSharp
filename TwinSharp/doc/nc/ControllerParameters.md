# ControllerParameters `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.NC
  TwinSharp.NC.ControllerParameters[[ControllerParameters]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `uint` | [`ID`](#id) | `get` |
| `string` | [`Name`](#name) | `get` |
| `double` | [`ProportionalGainKpOrKv`](#proportionalgainkporkv) | `get, set` |
| [`ControllerType`](./ControllerType.md) | [`Type`](#type) | `get` |

## Details
### Constructors
#### ControllerParameters
[*Source code*](https://github.com///blob//TwinSharp/NC/ControllerParameters.cs#L10)
```csharp
internal ControllerParameters(AdsClient client, uint id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | client |   |
| `uint` | id |   |

### Properties
#### ID
```csharp
public uint ID { get; }
```

#### Name
```csharp
public string Name { get; }
```

#### Type
```csharp
public ControllerType Type { get; }
```

#### ProportionalGainKpOrKv
```csharp
public double ProportionalGainKpOrKv { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
