using Fixer.Processes.Configurations;
using System.Collections.Generic;

namespace Fixer.Processes.Actions.Services
{
    internal interface IProcessActionService
    {
        void PerformAction(IProcessConfiguration processConfiguration, string action);
        void PerformActions(IProcessConfiguration processConfiguration, IEnumerable<string> actions);
    }
}
