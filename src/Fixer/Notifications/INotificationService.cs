using Fixer.Applications.Configurations;
using Fixer.Processes.Configurations;
using Fixer.Processes.States;

namespace Fixer.Notifications
{
    internal interface INotificationService
    {
        void NotifyContacts(
            IApplicationConfiguration applicationConfiguration,
            IProcessConfiguration processConfiguration,
            IProcessState processState,
            ICondition condition);
    }
}
