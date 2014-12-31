using System;

namespace Fixer.Processes.Configurations
{
    internal class Stop : IStop
    {
        public readonly static Stop Default = new Stop(TimeSpan.Zero);

        public TimeSpan Duration { get; private set; }

        public Stop(TimeSpan duration)
        {
            Duration = duration < TimeSpan.Zero ? TimeSpan.Zero : duration;
        }
    }
}
