using System;

namespace Fixer.Processes.Configurations
{
    internal class Script : IScript
    {
        public readonly static Script Default = new Script(string.Empty, string.Empty);

        public string Language { get; private set; }
        public string Source { get; private set; }

        public Script(string language, string text)
        {
            Language = string.IsNullOrWhiteSpace(language) ? string.Empty : language.Trim();
            Source = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }
    }
}
