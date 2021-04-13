using System;
using Newtonsoft.Json;
using Serialization.SubclassJsonConverter.Core;

namespace Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesDifferentStructuresCase
{
    [JsonConverter(typeof(Core.SubclassJsonConverter), typeof(NameIdentity))]
    internal class NameIdentity {
        public string Name { get; set; }
    }

    internal class Employee : NameIdentity {
        public int Id { get; set; }
        public string Division { get; set; }
    }

    internal class Customer : NameIdentity {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}