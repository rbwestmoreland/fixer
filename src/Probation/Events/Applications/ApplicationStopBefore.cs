using Probation.Applications;

namespace Probation.Events.Applications
{
    internal class ApplicationStopBefore : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationStopBefore(IApplication application)
        {
            Application = application;
        }
    }
}
