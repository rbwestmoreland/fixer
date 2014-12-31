using Probation.Applications;

namespace Probation.Events.Applications
{
    internal class ApplicationStartBefore : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationStartBefore(IApplication application)
        {
            Application = application;
        }
    }
}
