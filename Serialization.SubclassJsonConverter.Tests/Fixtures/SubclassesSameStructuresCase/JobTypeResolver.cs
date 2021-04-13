using System;
using Newtonsoft.Json.Linq;
using Serialization.SubclassJsonConverter.Core.Abstractions;
using Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesDifferentStructuresCase;

namespace Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesSameStructuresCase
{
    public class JobTypeResolver : ITypeResolver
    {
        public Type Resolve(JObject obj)
        {
            var typeProperty = obj.Property("type", StringComparison.InvariantCultureIgnoreCase);
            if(typeProperty is null) {
                throw new ArgumentException($"No type property available on payload to determine the type. Current payload: {obj}");
            }

            var resolvingPropertyName = typeProperty.Value.Value<string>();

            return resolvingPropertyName switch {
                var name when name == "SpecialJob" => typeof(CorrectDeserializableSpecialJob),
                var name when name == "RegularJob" => typeof(CorrectDeserializableRegularJob),
                _ => null
            };
        }
    }
}