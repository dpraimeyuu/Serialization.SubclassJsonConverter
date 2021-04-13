using System;
using Newtonsoft.Json;
using Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesSameStructuresCase;

namespace Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesDifferentStructuresCase
{
    [JsonConverter(typeof(Core.SubclassJsonConverter), typeof(BrokenDeserializableJob))]
    public class BrokenDeserializableJob
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }

    public class BrokenDeserializableRegularJob : BrokenDeserializableJob
    {
        public bool HasCompleted { get; set; }
    }

    public class BrokenDeserializableSpecialJob : BrokenDeserializableJob
    {
        public bool HasCompleted { get; set; }
    }

    [JsonConverter(typeof(Core.SubclassJsonConverter), typeof(CorrectDeserializableJob), typeof(JobTypeResolver))]
    public class CorrectDeserializableJob
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }

    public class CorrectDeserializableRegularJob : CorrectDeserializableJob
    {
        public bool HasCompleted { get; set; }
    }

    public class CorrectDeserializableSpecialJob : CorrectDeserializableJob
    {
        public bool HasCompleted { get; set; }
    }
}