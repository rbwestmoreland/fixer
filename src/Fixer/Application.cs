using Fixer.Applications;
using Fixer.Applications.Configurations.Factories;
using Fixer.Notifications;
using Fixer.Processes.Actions.Services;
using Fixer.Processes.Conditions.Services;
using Fixer.Processes.Conditions.Specifications.Factories;
using Fixer.Processes.Configurations.Factories;
using Fixer.Processes.States.Repositories;
using System;

namespace Fixer
{
    public static class Application
    {
        private static ApplicationConfiguration _configuration;
        private static IApplication _instance;

        public static void Initialize(Action<ApplicationConfiguration> configure)
        {
            if (_instance != null)
                throw new InvalidOperationException("You may not call 'Application.Initialize(...)' after calling 'Application.Start()'");

            _configuration = new ApplicationConfiguration();
            configure(_configuration);
        }

        public static void Start()
        {
            if (_instance != null)
                return;

            _instance = CreateApplication();
            _instance.Start();
        }

        public static void Stop()
        {
            if (_instance == null)
                return;

            _instance.Dispose();
            _instance = null;
        }

        private static IApplication CreateApplication()
        {
            var applicationConfigurationPath = _configuration.ApplicationConfigurationPath;
            var applicationConfigurationFactory = new ApplicationConfigurationFactory();
            var processConfigurationFactory = new ProcessConfigurationFactory();
            var processConditionSpecificationFactory = new ProcessConditionSpecificationFactory();
            var processConditionService = new ProcessConditionService(processConditionSpecificationFactory);
            var processStateRepository = new ProcessStateRepository();
            var processActionService = new ProcessActionService();
            var notificationService = new NotificationService();

            return new Applications.Application(
                applicationConfigurationPath,
                applicationConfigurationFactory,
                processConfigurationFactory,
                processStateRepository,
                processConditionService,
                processActionService,
                notificationService);
        }
    }
}
