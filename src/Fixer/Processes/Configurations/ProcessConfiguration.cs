using System.Collections.Generic;
using System.Linq;

namespace Fixer.Processes.Configurations
{
    internal class ProcessConfiguration : IProcessConfiguration
    {
        public string Path { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<string> Groups { get; private set; }
        public IStart Start { get; private set; }
        public IStop Stop { get; private set; }
        public IEnumerable<IContact> Contacts { get; private set; }
        public IEnumerable<ICondition> Conditions { get; private set; }

        public ProcessConfiguration(
            string path,
            string description,
            IEnumerable<string> groups,
            IStart start,
            IStop stop,
            IEnumerable<IContact> contacts,
            IEnumerable<ICondition> conditions)
        {
            Path = string.IsNullOrWhiteSpace(path) ? string.Empty : path;
            Description = string.IsNullOrWhiteSpace(description) ? string.Empty : description.Trim();
            Groups = groups ?? Enumerable.Empty<string>();
            Start = start ?? Fixer.Processes.Configurations.Start.Default;
            Stop = stop ?? Fixer.Processes.Configurations.Stop.Default;
            Contacts = contacts ?? Enumerable.Empty<IContact>();
            Conditions = conditions ?? Enumerable.Empty<ICondition>();
        }
    }
}
