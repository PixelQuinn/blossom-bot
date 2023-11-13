using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Config file to read from output so your token is not exposed
namespace BlossomBot.config
{
    internal class configlink
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
                string json = await sr.ReadToEndAsync();
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
