using Probation.Applications.Configurations;
using Probation.Processes.Configurations;
using Probation.Processes.States;

namespace Probation.Notifications
{
    internal class NotificationService : INotificationService
    {
        public void NotifyContacts(
            IApplicationConfiguration applicationConfiguration,
            IProcessConfiguration processConfiguration,
            IProcessState processState,
            ICondition condition)
        {
            //todo
        }
    }
}
