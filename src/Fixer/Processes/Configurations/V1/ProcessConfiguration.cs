using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Fixer.Processes.Configurations.V1
{
    internal class ProcessConfiguration
    {
        public string Description { get; set; }
        public string[] Groups { get; set; }
        public Start Start { get; set; }
        public Stop Stop { get; set; }
        public Contact[] Contacts { get; set; }
        public Condition[] Conditions { get; set; }

        public static bool TryLoad(string path, out ProcessConfiguration instance)
        {
            instance = default(ProcessConfiguration);

            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return false;

                if (!File.Exists(path))
                    return false;

                var json = File.ReadAllText(path);
                instance = JsonConvert.DeserializeObject<ProcessConfiguration>(json);
                return true;
            }
            catch { }

            return false;
        }
    }
}
