using Probation.Loggers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Probation
{
    public sealed class ApplicationConfiguration
    {
        private List<ILogger> _loggers;

        internal string ApplicationConfigurationPath { get; private set; }

        public ApplicationConfiguration()
        {
            _loggers = new List<ILogger>();

            SetDefaults();
        }

        private void SetDefaults()
        {
            UseDefaultApplicationConfigurationPath();
        }

        #region application configuration path

        public ApplicationConfiguration UseDefaultApplicationConfigurationPath()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyPath);

            ApplicationConfigurationPath = Path.Combine(assemblyDirectory, "probation.json");

            return this;
        }

        public ApplicationConfiguration UseApplicationConfigurationPath(string path)
        {
            ApplicationConfigurationPath = path;

            return this;
        }

        #endregion application configuration path

        #region logging

        public ApplicationConfiguration UseConsoleLogger()
        {
            var hasConsoleLogger = _loggers.Any(l => l.GetType() == typeof(ConsoleLogger));

            if(!hasConsoleLogger)
                _loggers.Add(new ConsoleLogger());

            return this;
        }

        #endregion logging
    }
}
