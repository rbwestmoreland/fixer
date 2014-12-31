using System;
using System.Collections.Generic;
using System.Linq;

namespace Fixer.Processes.Configurations
{
    internal class Condition : ICondition
    {
        public string Decription { get; private set; }
        public TimeSpan Interval { get; private set; }
        public IEnumerable<int> Ratio { get; private set; }
        public IScript Script { get; private set; }
        public string Action { get; private set; }
        public IEnumerable<string> Contact { get; private set; }

        public Condition(string description, TimeSpan interval, IEnumerable<int> ratio, IScript script, string action, IEnumerable<string> contact)
        {
            Decription = string.IsNullOrWhiteSpace(description) ? string.Empty : description.Trim();
            Interval = interval < TimeSpan.FromSeconds(5) ? TimeSpan.FromSeconds(5) : interval;
            Ratio = (ratio ?? new[] { 1, 1 }).Select(i => Math.Max(i, 1)).Select(i => Math.Min(i, 25));
            Script = script ?? Fixer.Processes.Configurations.Script.Default;
            Action = string.IsNullOrWhiteSpace(action) ? string.Empty : action.Trim();
            Contact = (contact ?? Enumerable.Empty<string>()).Select(c => string.IsNullOrWhiteSpace(c) ? string.Empty : c.Trim());
        }
    }
}
