using System.Linq;
using Kralizek.Assembla.Connector.Spaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Tests.Connector.Spaces
{
    [TestFixture]
    public class TabListJsonConverterTests
    {
        public class TestType
        {
            [JsonConverter(typeof(TabListJsonConverter))]
            public string[] Tabs { get; set; }
        }

        [Test, CustomAutoData]
        public void String_arrays_can_be_serialized(TestType testItem)
        {
            var json = JsonConvert.SerializeObject(testItem);

            JObject jObject = JObject.Parse(json);

            var values = jObject["Tabs"].Values<string>().ToArray();

            CollectionAssert.AreEquivalent(testItem.Tabs, values);

            Assert.That(values, Is.EquivalentTo(testItem.Tabs));
        }

        [Test, CustomAutoData]
        public void Custom_encoded_string_arrays_can_be_deserialized(string[] strings)
        {
            var json = $"{{\"Tabs\":\"---\\n{string.Join("", strings.Select(s => $"- {s}\\n"))}\"}}";

            var newItem = JsonConvert.DeserializeObject<TestType>(json);

            Assert.That(newItem.Tabs, Is.EquivalentTo(strings));
        }
    }
}
