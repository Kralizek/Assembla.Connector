using Kralizek.Assembla.Connector.Tickets.CustomFields;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests.Connector.Tickets.CustomFields
{
    [TestFixture]
    public class ListStringJsonConverterTests
    {
        [Test, CustomAutoData]
        public void String_arrays_can_be_deserialized(string[] strings)
        {
            var json = $"{{\"Items\":[\"{string.Join("\",\"", strings)}\"]}}";

            var deserialized = JsonConvert.DeserializeObject<TestType>(json);

            Assert.That(deserialized.Items, Is.EquivalentTo(strings));
        }

        [Test,CustomAutoData]
        public void String_array_is_serialized_to_comma_separated_string(TestType item)
        {
            var expected = $"{{\"Items\":\"{string.Join(",", item.Items)}\"}}";

            var serialized = JsonConvert.SerializeObject(item);

            Assert.That(serialized, Is.EqualTo(expected));
        }

        public class TestType
        {
            [JsonConverter(typeof(ListStringJsonConverter))]
            public string[] Items { get; set; }
        }
    }
}