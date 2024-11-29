# FeedOverride `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.CNC
  TwinSharp.CNC.FeedOverride[[FeedOverride]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `ushort` | [`ActualFeedOverride`](#actualfeedoverride) | `get` |
| `ushort` | [`CommandedFeedOverride`](#commandedfeedoverride) | `get, set` |

## Details
### Constructors
#### FeedOverride
[*Source code*](https://github.com///blob//TwinSharp/CNC/CncChannel.cs#L287)
```csharp
internal FeedOverride(AdsClient comClient, int channelNumber)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | comClient |   |
| `int` | channelNumber |   |

### Properties
#### CommandedFeedOverride
```csharp
public ushort CommandedFeedOverride { get; set; }
```

#### ActualFeedOverride
```csharp
public ushort ActualFeedOverride { get; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
