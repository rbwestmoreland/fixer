using System.Collections.Generic;
using System.Linq;

namespace Probation.Applications.Configurations
{
    internal class ApplicationConfiguration : IApplicationConfiguration
    {
        public string Path { get; private set; }
        public IEnumerable<string> Files { get; private set; }

        public ApplicationConfiguration(
            string path,
            IEnumerable<string> files)
        {
            Files = files ?? Enumerable.Empty<string>();
        }
    }
}
