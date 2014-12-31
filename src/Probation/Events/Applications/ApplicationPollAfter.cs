using Probation.Applications;

namespace Probation.Events.Applications
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
