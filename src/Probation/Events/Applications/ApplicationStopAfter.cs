using Probation.Applications;

namespace Probation.Events.Applications
{
    internal class ApplicationStopAfter : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationStopAfter(IApplication application)
        {
            Application = application;
        }
    }
}
