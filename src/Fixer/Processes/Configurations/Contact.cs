using System.Collections.Generic;
using System.Linq;

namespace Fixer.Processes.Configurations
{
    internal class Contact : IContact
    {
        public string Name { get; private set; }
        public IEnumerable<string> Groups { get; private set; }
        public IDictionary<string, string> Addresses { get; private set; }

        public Contact(string name, IEnumerable<string> groups, IDictionary<string, string> addresses)
        {
            Name = string.IsNullOrWhiteSpace(name) ? string.Empty : name.Trim();
            Groups = (groups ?? Enumerable.Empty<string>()).Select(group => string.IsNullOrWhiteSpace(group) ? string.Empty : group.Trim());
            Addresses = (addresses ?? new Dictionary<string, string>());
        }
    }
}
