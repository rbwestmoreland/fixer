using Fixer.Processes.Configurations;

namespace Fixer.Processes.Actions.Services
{
    internal interface IProcessActionService
    {
        void PerformAction(IProcessConfiguration processConfiguration, string action);
    }
}
