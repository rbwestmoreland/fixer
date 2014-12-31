using Newtonsoft.Json;
using System.IO;

namespace Fixer.Applications.Configurations.V1
{
    internal class ApplicationConfigurationV1
    {
        public string[] Files { get; set; }

        public static bool TryLoad(string path, out ApplicationConfigurationV1 instance)
        {
            instance = default(ApplicationConfigurationV1);

            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return false;

                if (!File.Exists(path))
                    return false;

                var json = File.ReadAllText(path);
                instance = JsonConvert.DeserializeObject<ApplicationConfigurationV1>(json);
                return true;
            }
            catch { }

            return false;
        }
    }
}
