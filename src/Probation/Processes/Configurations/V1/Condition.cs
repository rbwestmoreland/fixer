using System;

namespace Probation.Processes.Configurations.V1
{
    internal class Condition
    {
        public string Description { get; set; }
        public int? Interval { get; set; }
        public int[] Ratio { get; set; }
        public Script Script { get; set; }
        public string Action { get; set; }
        public string[] Contact { get; set; }
    }
}
