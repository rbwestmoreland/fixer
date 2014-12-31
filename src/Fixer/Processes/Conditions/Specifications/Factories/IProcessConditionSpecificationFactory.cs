using Fixer.Processes.Configurations;

namespace Fixer.Processes.Conditions.Specifications.Factories
{
    internal interface IProcessConditionSpecificationFactory
    {
        IProcessConditionSpecification Create(ICondition condition);
    }
}
