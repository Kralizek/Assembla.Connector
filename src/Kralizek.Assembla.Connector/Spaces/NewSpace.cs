using System;
using Newtonsoft.Json;

namespace Assembla.Spaces
{
    public class NewSpace
    {
        public NewSpace(string wikiName)
        {
            if (string.IsNullOrEmpty(wikiName))
            {
                throw new ArgumentNullException(nameof(wikiName));
            }

            WikiName = wikiName;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("wiki_name")]
        public string WikiName { get; set; }
    }
}