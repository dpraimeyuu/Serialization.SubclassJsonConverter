using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serialization.SubclassJsonConverter.Core.Abstractions;

namespace Serialization.SubclassJsonConverter.Core
{
    public class SubclassJsonConverter : JsonConverter
    {
        private readonly Type _baseType;
        private readonly ITypeResolver _typeResolver;
        private readonly IEnumerable<Type> _subTypes;

        public SubclassJsonConverter(Type baseType)
        {
            _baseType = baseType;
            _subTypes = FindSubclassTypes(_baseType);
        }

        public SubclassJsonConverter(Type convertSubtypesOfType, Type typeResolver = null)
        {
            _baseType = convertSubtypesOfType;
            _typeResolver =
                typeResolver != null
                    ? Activator.CreateInstance(typeResolver) as ITypeResolver
                    : null;

            _subTypes = FindSubclassTypes(_baseType);
        }

        private IEnumerable<Type> FindSubclassTypes(Type baseType) =>
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes(), (_, assemblyType) => assemblyType)
                .Where(t => _baseType.IsAssignableFrom(t) && t != baseType && !t.IsAbstract);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private Type ResolveType(JObject obj)
        {
            if (_typeResolver != null)
            {
                var resolvedType = _typeResolver.Resolve(obj);

                return resolvedType ?? FindSubtype(obj);
            }

            return FindSubtype(obj);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                return existingValue;
                
            var obj = JObject.Load(reader);
            var objectSubtype = ResolveType(obj);

            existingValue = Activator.CreateInstance(objectSubtype);
            serializer.Populate(obj.CreateReader(), existingValue);

            return existingValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(_baseType);
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;

        private Type FindSubtype(JObject jObj)
        {
            foreach (var subtype in _subTypes)
            {
                if (JsonHasPropertiesFromType(subtype, jObj))
                    return subtype;
            }
            return _baseType;
        }

        private static bool JsonHasPropertiesFromType(Type t, JObject json)
        {
            var jsonProperties = json.Properties().Select(jProp => jProp.Name.ToLowerInvariant());
            var typeProps = t.GetProperties().Select(prop => prop.Name.ToLowerInvariant());

            return jsonProperties.All(propName => typeProps.Contains(propName));
        }
    }
}
