using Probation.Processes.Configurations;
using Probation.Processes.States;
using System.Collections.Generic;

namespace Probation.Processes.Conditions.Services
{
    internal interface IProcessConditionService
    {
        IEnumerable<ICondition> FindActiveConditions(IProcessConfiguration processConfiguration, IProcessState processState);
    }
}
