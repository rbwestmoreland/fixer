using Fixer.Processes.States;

namespace Fixer.Processes.Conditions.Specifications.Null
{
    internal class NullProcessConditionSpecification : IProcessConditionSpecification
    {
        public bool IsSatisfiedBy(IProcessState processState)
        {
            return false;
        }
    }
}
