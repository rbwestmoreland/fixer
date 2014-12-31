using Fixer.Applications;

namespace Fixer.Events.Applications
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
