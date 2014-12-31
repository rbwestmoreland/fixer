using Fixer.Applications;

namespace Fixer.Events.Applications
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
