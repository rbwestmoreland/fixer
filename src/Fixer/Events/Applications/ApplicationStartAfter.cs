using Fixer.Applications;

namespace Fixer.Events.Applications
{
    internal class ApplicationStartAfter : IEvent
    {
        public IApplication Application { get; private set; }

        public ApplicationStartAfter(IApplication application)
        {
            Application = application;
        }
    }
}
