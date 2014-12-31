using Fixer.Events;
using Fixer.Events.Processes.Conditions;
using Fixer.Processes.Conditions.Specifications.Factories;
using Fixer.Processes.Configurations;
using Fixer.Processes.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fixer.Processes.Conditions.Services
{
    internal class ProcessConditionService : IProcessConditionService
    {
        private List<Check> _history;
        private IProcessConditionSpecificationFactory _processConditionSpecificationFactory;

        public ProcessConditionService(IProcessConditionSpecificationFactory processConditionSpecificationFactory)
        {
            _history = new List<Check>();
            _processConditionSpecificationFactory = processConditionSpecificationFactory;
        }

        public IEnumerable<ICondition> FindActiveConditions(IProcessConfiguration processConfiguration, IProcessState processState)
        {
            var conditions = new List<ICondition>();

            foreach (var condition in processConfiguration.Conditions)
            {
                if (IsActive(processConfiguration, processState, condition))
                {
                    EventBus.Raise(new ProcessConditionMet(processConfiguration, processState, condition));
                    conditions.Add(condition);
                }
            }

            return conditions;
        }

        private bool IsActive(IProcessConfiguration processConfiguration, IProcessState processState, ICondition condition)
        {
            var isActive = false;
            var isTimeToCheck = IsTimeToCheck(processConfiguration, condition);

            if (isTimeToCheck)
            {
                CheckCondition(processConfiguration, processState, condition);
                isActive = HasOccurredEnough(processConfiguration, condition);
            }

            return isActive;
        }

        private bool IsTimeToCheck(IProcessConfiguration processConfiguration, ICondition condition)
        {
            var lastCheck = _history
                .Where(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path))
                .Where(c => c.ProcessConditionDescription.Equals(condition.Decription))
                .OrderByDescending(c => c.Timestamp)
                .ToArray()
                .FirstOrDefault();

            var timeSinceLastCheck = lastCheck == null ? TimeSpan.MaxValue : DateTime.UtcNow - lastCheck.Timestamp;
            var shouldCheck = timeSinceLastCheck > condition.Interval;

            return shouldCheck;
        }

        private void CheckCondition(IProcessConfiguration processConfiguration, IProcessState processState, ICondition condition)
        {
            var specification = _processConditionSpecificationFactory.Create(condition);
            var hasOccured = specification.IsSatisfiedBy(processState);

            _history.Add(new Check
            {
                ProcessConfigurationPath = processConfiguration.Path,
                ProcessConditionDescription = condition.Decription,
                Timestamp = DateTime.UtcNow,
                Occured = hasOccured
            });

            var maxToKeep = condition.Ratio.ElementAt(1);
            var toRemove = _history.Where(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path))
                .Where(c => c.ProcessConditionDescription.Equals(condition.Decription))
                .OrderByDescending(c => c.Timestamp)
                .ToArray()
                .Skip(maxToKeep);

            foreach (var item in toRemove)
            {
                _history.Remove(item);
            }
        }

        private bool HasOccurredEnough(IProcessConfiguration processConfiguration, ICondition condition)
        {
            var times = condition.Ratio.ElementAt(0);
            var window = condition.Ratio.ElementAt(1);

            var previousChecks = _history.Where(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path))
                .Where(c => c.ProcessConditionDescription.Equals(condition.Decription))
                .OrderByDescending(c => c.Timestamp)
                .ToArray()
                .Take(window);

            var occurrences = previousChecks
                .Where(c => c.Occured)
                .Count();

            var hasOccuredEnough = occurrences >= times;

            if (hasOccuredEnough)
                _history.RemoveAll(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path) && c.ProcessConditionDescription.Equals(condition.Decription));

            return hasOccuredEnough;
        }

        private class Check
        {
            public string ProcessConfigurationPath { get; set; }
            public string ProcessConditionDescription { get; set; }
            public DateTime Timestamp { get; set; }
            public bool Occured { get; set; }
        }
    }
}
