using System.Collections.Generic;

namespace Fixer.Processes.Configurations.V1
{
    internal class Contact
    {
        public string Name { get; set; }
        public string[] Groups { get; set; }
        public Dictionary<string, string> Addresses { get; set; }
    }
}
