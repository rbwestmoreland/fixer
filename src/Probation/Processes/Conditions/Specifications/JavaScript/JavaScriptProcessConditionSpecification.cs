using Jint;
using Probation.Processes.Configurations;
using Probation.Processes.States;

namespace Probation.Processes.Conditions.Specifications.JavaScript
{
    internal class JavaScriptProcessConditionSpecification : IProcessConditionSpecification
    {
        private readonly ICondition _condition;

        public JavaScriptProcessConditionSpecification(ICondition condition)
        {
            _condition = condition;
        }

        public bool IsSatisfiedBy(IProcessState processState)
        {
            var isSatisfiedBy = false;

            try
            {
                //todo: finalize
                var result = new Engine()
                    .SetValue("state", processState.IsRunning ? "started" : "stopped")
                    .SetValue("cpu_usage", processState.CpuUsage)
                    .SetValue("memory_usage", processState.MemoryUsage)
                    .SetValue("uptime", processState.Uptime.TotalMinutes)
                    .Execute(_condition.Script.Source)
                    .GetCompletionValue();

                if (result.IsBoolean())
                    isSatisfiedBy = result.AsBoolean();
            }
            catch { }

            return isSatisfiedBy;
        }
    }
}
