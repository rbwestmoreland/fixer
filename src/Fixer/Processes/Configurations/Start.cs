using System;

namespace Fixer.Processes.Configurations
{
    internal class Start : IStart
    {
        public readonly static Start Default = new Start(string.Empty, string.Empty);

        public string File { get; private set; }
        public string Arguments { get; private set; }

        public Start(string file, string arguments)
        {
            File = string.IsNullOrWhiteSpace(file) ? string.Empty : file;
            Arguments = string.IsNullOrWhiteSpace(arguments) ? string.Empty : arguments;
        }
    }
}
