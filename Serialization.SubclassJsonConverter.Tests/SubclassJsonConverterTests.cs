using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Serialization.SubclassJsonConverter.Tests.Fixtures.SubclassesDifferentStructuresCase;
using Xunit;

namespace Serialization.SubclassJsonConverter.Tests
{
    public class SubclassJsonConverterTests
    {
        [Fact]
        public void Given_Employee_Subclass_Payload_When_Deserializing_Then_Correct_Type_Is_Created()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesDifferentStructuresCase/Payloads/correct_employee.json");
            var employee = JsonConvert.DeserializeObject<Employee>(payload);

            employee.Should().BeOfType<Employee>();
        }

        [Fact]
        public void Given_Customer_Subclass_Payload_When_Deserializing_Then_Correct_Type_Is_Created()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesDifferentStructuresCase/Payloads/correct_customer.json");
            var customer = JsonConvert.DeserializeObject<Customer>(payload);

            customer.Should().BeOfType<Customer>();
        }

        [Fact]
        public void Given_RegularJob_Subclass_Payload_When_Deserializing_Without_Type_Resolver_Then_Correct_Type_Is_Created()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/regular_job.json");
            var regularJob = JsonConvert.DeserializeObject<BrokenDeserializableRegularJob>(payload);

            regularJob.Should().BeOfType<BrokenDeserializableRegularJob>();
        }

        [Fact]
        public void Given_SpecialJob_Subclass_Payload_When_Deserializing_Without_Type_Resolver_Then_Throws_InvalidCastException()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/special_job.json");
            Assert.Throws<InvalidCastException>(() => JsonConvert.DeserializeObject<BrokenDeserializableSpecialJob>(payload));
        }

        [Fact]
        public void Given_RegularJob_Subclass_Payload_When_Deserializing_With_Type_Resolver_Then_Correct_Type_Is_Created()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/regular_job.json");
            var regularJob = JsonConvert.DeserializeObject<CorrectDeserializableRegularJob>(payload);

            regularJob.Should().BeOfType<CorrectDeserializableRegularJob>();
        }

        [Fact]
        public void Given_SpecialJob_Subclass_Payload_When_Deserializing_With_Type_Resolver_Then_Correct_Type_Is_Created()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/special_job.json");
            var specialJob = JsonConvert.DeserializeObject<CorrectDeserializableSpecialJob>(payload);

            specialJob.Should().BeOfType<CorrectDeserializableSpecialJob>();
        }

        [Fact]
        public void Given_SpecialJob_Subclass_Payload_Without_Type_Property_Defined_When_Deserializing_With_Type_Resolver_Then_ArgumentException_Is_Thrown()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/job_without_type_property.json");
            Assert.Throws<ArgumentException>(() => JsonConvert.DeserializeObject<CorrectDeserializableSpecialJob>(payload));
        }

        [Fact]
        public void Given_SpecialJob_Subclass_Payload_With_Unknown_Type_Property_When_Deserializing_With_Type_Resolver_Then_InvalidCastException_Is_Thrown()
        {
            var payload = File.ReadAllText("./Fixtures/SubclassesSameStructuresCase/Payloads/job_unknown_type_property.json");
            Assert.Throws<InvalidCastException>(() => JsonConvert.DeserializeObject<CorrectDeserializableSpecialJob>(payload));
        }
    }
}