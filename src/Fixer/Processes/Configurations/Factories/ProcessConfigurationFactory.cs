using Fixer.Events;
using Fixer.Events.Processes.Configurations;
using System;
using System.Linq;

namespace Fixer.Processes.Configurations.Factories
{
    internal class ProcessConfigurationFactory : IProcessConfigurationFactory
    {
        public IProcessConfiguration Create(string path)
        {
            var processConfiguration = default(IProcessConfiguration);

            EventBus.Raise(new ProcessConfigurationLoadBefore(path));

            V1.ProcessConfiguration v1;
            if (V1.ProcessConfiguration.TryLoad(path, out v1))
            {
                var description = v1.Description;
                var groups = v1.Groups;
                var start = v1.Start == null ? Start.Default : new Start(v1.Start.File, v1.Start.Arguments);
                var stop = v1.Stop == null ? Stop.Default : new Stop(TimeSpan.FromSeconds(v1.Stop.Duration));
                var contacts = v1.Contacts.Select(c => new Contact(c.Name, c.Groups, c.Addresses));
                var conditions = v1.Conditions.Select(c =>
                    {
                        var script = c.Script == null ? Script.Default : new Script(c.Script.Language, c.Script.Source);
                        var interval = TimeSpan.FromSeconds(c.Interval.GetValueOrDefault(60));
                        var ratio = (c.Ratio ?? new[] { 1, 1 }).Take(2);
                        return new Condition(c.Description, interval, ratio, script, c.Action, c.Contact);
                    });

                processConfiguration = new ProcessConfiguration(path, description, groups, start, stop, contacts, conditions);
            }

            EventBus.Raise(new ProcessConfigurationLoadAfter(path, processConfiguration));

            return processConfiguration;
        }
    }
}
