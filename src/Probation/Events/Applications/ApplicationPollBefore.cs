using Probation.Applications;

namespace Probation.Events.Applications
{
    internal class ApplicationPollBefore : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationPollBefore(IApplication application)
        {
            Application = application;
        }
    }
}
