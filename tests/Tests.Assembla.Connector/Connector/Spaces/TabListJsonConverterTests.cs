using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kralizek.Assembla.Connector.Spaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Tests.Assembla.Connector.Spaces
{
    public class TabListJsonConverterTests
    {
        public class TestType
        {
            [JsonConverter(typeof(TabListJsonConverter))]
            public string[] Tabs { get; set; }
        }

        [Fact]
        public void String_arrays_can_be_serialized()
        {
            TestType testType = new TestType
            {
                Tabs = new[] {"hello", "world"}
            };

            var json = JsonConvert.SerializeObject(testType);

            JObject jObject = JObject.Parse(json);

            var values = jObject["Tabs"].Values<string>().ToArray();

            values.ShouldContain(c => c == testType.Tabs[0]);
            values.ShouldContain(c => c == testType.Tabs[1]);
        }

        [Fact]
        public void Custom_encoded_string_arrays_can_be_deserialized()
        {
            string json = "{\"Tabs\": \"---\\n- hello\\n- world\\n\"}";

            var newItem = JsonConvert.DeserializeObject<TestType>(json);

            newItem.Tabs.ShouldContain(c => c == "hello");
            newItem.Tabs.ShouldContain(c => c == "world");
        }
    }
}
