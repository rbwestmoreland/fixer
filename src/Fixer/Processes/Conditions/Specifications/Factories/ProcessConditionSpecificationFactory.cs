using Fixer.Processes.Conditions.Specifications.JavaScript;
using Fixer.Processes.Conditions.Specifications.Null;
using Fixer.Processes.Configurations;
using System;
using System.Linq;

namespace Fixer.Processes.Conditions.Specifications.Factories
{
    internal class ProcessConditionSpecificationFactory : IProcessConditionSpecificationFactory
    {
        public IProcessConditionSpecification Create(ICondition condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            if (condition.Script == null)
                throw new ArgumentNullException("condition.Script");

            IProcessConditionSpecification specification = new NullProcessConditionSpecification();

            if (IsMatch(condition.Script.Language, new [] { "javascript", "js" }))
                specification = new JavaScriptProcessConditionSpecification(condition);

            return specification;
        }

        private static bool IsMatch(string value, string[] values)
        {
            return values.Any(v => string.Equals(v, value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
