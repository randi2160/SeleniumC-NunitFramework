using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace roomstogoseleniumframework.Utilities
{
    public class JsonReader
    {
        public JsonReader() { }

        public List<string> ExtractSearchTerms()
        {
            string jsonFilePath = @"utilities\testData.json";

            // Debug log to ensure the correct path
            Console.WriteLine("Looking for JSON file at: " + Path.GetFullPath(jsonFilePath));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON file not found at " + jsonFilePath);
            }

            string myJsonString = File.ReadAllText(jsonFilePath);
            var jsonObject = JToken.Parse(myJsonString);

            var searchTerms = jsonObject.SelectToken("TestData")?.Select(t => t["searchTerm"]?.Value<string>()).ToList();

            if (searchTerms == null || searchTerms.Count == 0)
            {
                throw new ArgumentNullException("No search terms found in the JSON file.");
            }

            return searchTerms;
        }
    }
}
