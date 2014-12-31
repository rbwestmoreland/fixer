using Fixer.Processes.States;

namespace Fixer.Processes.Conditions.Specifications
{
    internal interface IProcessConditionSpecification
    {
        bool IsSatisfiedBy(IProcessState processState);
    }
}
