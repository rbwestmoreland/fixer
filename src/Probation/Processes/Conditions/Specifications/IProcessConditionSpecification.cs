using Probation.Processes.States;

namespace Probation.Processes.Conditions.Specifications
{
    internal interface IProcessConditionSpecification
    {
        bool IsSatisfiedBy(IProcessState processState);
    }
}
