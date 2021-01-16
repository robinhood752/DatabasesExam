using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DepartmentMotorVehicles
{
    public class ConfigurationManager
    {
        public string ConnectionString { get; }

        public ConfigurationManager()
        {
            var configText = File.OpenText(Path.Combine(Environment.CurrentDirectory, "app.config.json")).ReadToEnd();
            var configJson = (JObject) JsonConvert.DeserializeObject(configText);
            ConnectionString = configJson?["department-motor-vehicles "]?["connection-string"]!.Value<string>();
        }
    }
}
