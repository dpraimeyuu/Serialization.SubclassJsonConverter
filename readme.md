## Installation
```sh
Install-Package Serialization.SubclassJsonConverter.Core
```

## TLDR
Have some inheritance you want to deserialize?
* use `SubclassJsonConverter` for subclasses with different structures
* use `SubclassJsonConverter` + your own type resolver implementing `ITypeResolver` interface for subclasses with same structure

## Why?
From time to time you need to model your data in an inferitance-fashion approach (doesn't matter if you chose such or were given ;-)) and then deserialization of the subclasses might be troublesome.

## How?
Nothing fancy - this package is supposed to solve this by exposing a simple `SubclassJsonConverter` that uses underyling subclass structure to determine the output type.

When structure is shared between different subclasses then it's not sufficient to use bare `SubclassJsonConverter`.

In order to work against that, deserialization needs some help to determine the underyling type. `ITypeResolver` interface is exposed so that it can be implemented in your resolver which then might be passed as an argument to the `JsonConverter` attribute on your base class.

## Usage
```csharp
using Newtonsoft.Json

[JsonConverter(typeof(SubclassJsonConverter), typeof(Job))]
public class Job
{
    public Guid Id { get; set; }
}

public RegularJob : Job
{
    public string Name { get; set; }
}

public SpecialJob : Job
{
    public string SpecialPropertyName { get; set; }
}
```
