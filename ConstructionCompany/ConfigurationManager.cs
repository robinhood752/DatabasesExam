using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConstructionCompany
{
    class ConfigurationManager
    {
        public string ConnectionString { get; }

        public ConfigurationManager()
        {
            var configText = File.OpenText(Path.Combine(Environment.CurrentDirectory, "app.config.json")).ReadToEnd();
            var configJson = (JObject)JsonConvert.DeserializeObject(configText);
            ConnectionString = configJson?["construction-company"]?["connection-string"]!.Value<string>();
        }
    }
}