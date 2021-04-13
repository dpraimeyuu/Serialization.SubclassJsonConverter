using System;
using Newtonsoft.Json.Linq;

namespace Serialization.SubclassJsonConverter.Core.Abstractions
{
    public interface ITypeResolver
    {
        Type Resolve(JObject obj);
    }
}
