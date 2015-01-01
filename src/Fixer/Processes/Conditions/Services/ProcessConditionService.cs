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
                    conditions.Add(condition);
                    EventBus.Raise(new ProcessConditionMet(processConfiguration, processState, condition));
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
            var occurred = specification.IsSatisfiedBy(processState);

            AddToHistory(processConfiguration, condition, occurred);

            var timestamp = DateTime.UtcNow;
            var timesOccurred = FindTimesOccurred(processConfiguration, condition);
            EventBus.Raise(new ProcessConditionChecked(processConfiguration, processState, condition, occurred, timesOccurred));
        }

        private bool HasOccurredEnough(IProcessConfiguration processConfiguration, ICondition condition)
        {
            var times = condition.Ratio.ElementAt(0);
            var occurrences = FindTimesOccurred(processConfiguration, condition);

            var hasOccuredEnough = occurrences >= times;

            if (hasOccuredEnough)
                ClearHistory(processConfiguration, condition);

            return hasOccuredEnough;
        }

        private int FindTimesOccurred(IProcessConfiguration processConfiguration, ICondition condition)
        {
            var window = condition.Ratio.ElementAt(1);

            var previousChecks = _history.Where(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path))
                .Where(c => c.ProcessConditionDescription.Equals(condition.Decription))
                .OrderByDescending(c => c.Timestamp)
                .ToArray()
                .Take(window);

            var occurrences = previousChecks
                .Where(c => c.Occurred)
                .Count();

            return occurrences;
        }

        private void AddToHistory(IProcessConfiguration processConfiguration, ICondition condition, bool occurred)
        {
            _history.Add(new Check
            {
                ProcessConfigurationPath = processConfiguration.Path,
                ProcessConditionDescription = condition.Decription,
                Timestamp = DateTime.UtcNow,
                Occurred = occurred
            });

            TrimHistory(processConfiguration, condition);
        }

        private void TrimHistory(IProcessConfiguration processConfiguration, ICondition condition)
        {
            var maxChecksToKeep = condition.Ratio.ElementAt(1);
            var checksToDiscard = _history.Where(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path))
                .Where(c => c.ProcessConditionDescription.Equals(condition.Decription))
                .OrderByDescending(c => c.Timestamp)
                .ToArray()
                .Skip(maxChecksToKeep);

            foreach (var check in checksToDiscard)
            {
                _history.Remove(check);
            }
        }

        private void ClearHistory(IProcessConfiguration processConfiguration, ICondition condition)
        {
            _history.RemoveAll(c => c.ProcessConfigurationPath.Equals(processConfiguration.Path) && c.ProcessConditionDescription.Equals(condition.Decription));
        }

        private class Check
        {
            public string ProcessConfigurationPath { get; set; }
            public string ProcessConditionDescription { get; set; }
            public DateTime Timestamp { get; set; }
            public bool Occurred { get; set; }
        }
    }
}
