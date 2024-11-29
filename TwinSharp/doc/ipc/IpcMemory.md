# IpcMemory `Public class`

## Description
The IpcMemory class provides methods to interact with the memory of a TwinCAT ADS device.
            It allows reading the allocated and available program memory, as well as storage memory and memory division.
            The class handles different subindexes for devices with more than 4 GB of RAM and WindowsCE devices.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph TwinSharp.IPC
  TwinSharp.IPC.IpcMemory[[IpcMemory]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `uint` | [`MemoryDivision`](#memorydivision)<br>Memory division.<br>            Only for WindowsCE. | `get` |
| `ulong` | [`ProgramMemoryAllocated`](#programmemoryallocated)<br>Program Memory Allocated. | `get` |
| `ulong` | [`ProgramMemoryAvailable`](#programmemoryavailable)<br>Program Memory Available. | `get` |
| `uint` | [`StorageMemoryAllocated`](#storagememoryallocated)<br>Storage Memory Allocated.<br>            Only for WindowsCE. | `get` |
| `uint` | [`StorageMemoryAvailable`](#storagememoryavailable)<br>Storage Memory Available.<br>            Only for WindowsCE. | `get` |

## Details
### Summary
The IpcMemory class provides methods to interact with the memory of a TwinCAT ADS device.
            It allows reading the allocated and available program memory, as well as storage memory and memory division.
            The class handles different subindexes for devices with more than 4 GB of RAM and WindowsCE devices.

### Constructors
#### IpcMemory
[*Source code*](https://github.com///blob//TwinSharp/IPC/IpcMemory.cs#L17)
```csharp
internal IpcMemory(AdsClient client, ushort mdpId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `AdsClient` | client |   |
| `ushort` | mdpId |   |

### Properties
#### ProgramMemoryAllocated
```csharp
public ulong ProgramMemoryAllocated { get; }
```
##### Summary
Program Memory Allocated.

#### ProgramMemoryAvailable
```csharp
public ulong ProgramMemoryAvailable { get; }
```
##### Summary
Program Memory Available.

#### StorageMemoryAllocated
```csharp
public uint StorageMemoryAllocated { get; }
```
##### Summary
Storage Memory Allocated.
            Only for WindowsCE.

#### StorageMemoryAvailable
```csharp
public uint StorageMemoryAvailable { get; }
```
##### Summary
Storage Memory Available.
            Only for WindowsCE.

#### MemoryDivision
```csharp
public uint MemoryDivision { get; }
```
##### Summary
Memory division.
            Only for WindowsCE.

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
