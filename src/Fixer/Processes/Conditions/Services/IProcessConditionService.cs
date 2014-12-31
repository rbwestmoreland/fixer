using Fixer.Processes.Configurations;
using Fixer.Processes.States;
using System.Collections.Generic;

namespace Fixer.Processes.Conditions.Services
{
    internal interface IProcessConditionService
    {
        IEnumerable<ICondition> FindActiveConditions(IProcessConfiguration processConfiguration, IProcessState processState);
    }
}
