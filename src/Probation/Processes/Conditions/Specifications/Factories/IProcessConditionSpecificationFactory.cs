using Probation.Processes.Configurations;

namespace Probation.Processes.Conditions.Specifications.Factories
{
    internal interface IProcessConditionSpecificationFactory
    {
        IProcessConditionSpecification Create(ICondition condition);
    }
}
