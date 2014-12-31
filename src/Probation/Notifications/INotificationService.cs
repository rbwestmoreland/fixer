using Probation.Applications.Configurations;
using Probation.Processes.Configurations;
using Probation.Processes.States;

namespace Probation.Notifications
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
