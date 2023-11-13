using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Config file to read from output so your token is not exposed
namespace BlossomBot.config
{
    internal class JSONReader
    {
        // Properties to store token and prefix
        public string token {  get; set; }
        public string prefix { get; set; }

        // Method to read configuration from JSON file
        public async Task ReadJSON()
        {
            // Using statement to automatically close the StreamReader when done
            using (StreamReader sr = new StreamReader("config.json"))
            {
                // Read the entire content asynchronously from the StreamReader and store it as a string
                string json = await sr.ReadToEndAsync();

                // Deserialize the JSON string into an object of type JSONStructure
                // JSONStructure should be a class or type representing the expected structure of the JSON data
                // JsonConvert.DeserializeObject<> is a method from the Newtonsoft.Json library (Json.NET)
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);
                
                this.token = data.token;
                this.prefix = data.prefix;
            }
        }
    }

    // Class representing the structure of JSON configuration
    internal sealed class JSONStructure
    {
        public string token { set; get; }
        public string prefix { set; get; }
    }
}
