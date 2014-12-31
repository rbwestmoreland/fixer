using Probation.Processes.States;

namespace Probation.Processes.Conditions.Specifications.Null
{
    internal class NullProcessConditionSpecification : IProcessConditionSpecification
    {
        public bool IsSatisfiedBy(IProcessState processState)
        {
            return false;
        }
    }
}
