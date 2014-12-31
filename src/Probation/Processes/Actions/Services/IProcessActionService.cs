using Probation.Processes.Configurations;

namespace Probation.Processes.Actions.Services
{
    internal interface IProcessActionService
    {
        void PerformAction(IProcessConfiguration processConfiguration, string action);
    }
}
