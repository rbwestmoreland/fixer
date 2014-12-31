using Fixer.Applications;

namespace Fixer.Events.Applications
{
    internal class ApplicationPollAfter : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationPollAfter(IApplication application)
        {
            Application = application;
        }
    }
}
