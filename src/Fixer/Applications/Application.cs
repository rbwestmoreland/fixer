using Fixer.Applications.Configurations.Factories;
using Fixer.Events;
using Fixer.Events.Applications;
using Fixer.Notifications;
using Fixer.Processes.Actions.Services;
using Fixer.Processes.Conditions.Services;
using Fixer.Processes.Configurations.Factories;
using Fixer.Processes.States.Repositories;
using System;
using System.Linq;
using System.Timers;

namespace Fixer.Applications
{
    internal class Application : IApplication
    {
        private string _applicationConfigurationPath;
        private readonly IApplicationConfigurationFactory _applicationConfigurationFactory;
        private readonly IProcessConfigurationFactory _processConfigurationFactory;
        private readonly IProcessStateRepository _processStateRepository;
        private readonly IProcessConditionService _processConditionService;
        private readonly IProcessActionService _processActionService;
        private readonly INotificationService _notificationService;
        private readonly Timer _timer;

        public Application(
            string applicationConfigurationPath,
            IApplicationConfigurationFactory applicationConfigurationFactory,
            IProcessConfigurationFactory processConfigurationFactory,
            IProcessStateRepository processStateRepository,
            IProcessConditionService processConditionSpervice,
            IProcessActionService processActionService,
            INotificationService notificationService)
        {
            _applicationConfigurationPath = applicationConfigurationPath;
            _applicationConfigurationFactory = applicationConfigurationFactory;
            _processConfigurationFactory = processConfigurationFactory;
            _processStateRepository = processStateRepository;
            _processConditionService = processConditionSpervice;
            _processActionService = processActionService;
            _notificationService = notificationService;

            _timer = new Timer();
            _timer.Elapsed += (sender, e) =>
                {
                    _timer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
                    _timer.Stop();
                    Poll();
                    _timer.Start();
                };
        }

        public void Start()
        {
            EventBus.Raise(new ApplicationStartBefore(this));

            _timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            _timer.Start();

            EventBus.Raise(new ApplicationStartAfter(this));
        }

        public void Stop()
        {
            EventBus.Raise(new ApplicationStopAfter(this));

            _timer.Stop();

            EventBus.Raise(new ApplicationStopAfter(this));
        }

        private void Poll()
        {
            EventBus.Raise(new ApplicationPollBefore(this));

            var applicationConfiguration = _applicationConfigurationFactory.Create(_applicationConfigurationPath);

            if (applicationConfiguration == null)
                return;

            var processConfigurations = applicationConfiguration.Files
                .Select(_processConfigurationFactory.Create)
                .Where(c => c != null);

            foreach (var processConfiguration in processConfigurations)
            {
                var processState = _processStateRepository.GetState(processConfiguration);
                var conditions = _processConditionService.FindActiveConditions(processConfiguration, processState);

                foreach (var condition in conditions)
                {
                    _processActionService.PerformAction(processConfiguration, condition.Action);
                    _notificationService.NotifyContacts(applicationConfiguration, processConfiguration, processState, condition);
                }
            }

            EventBus.Raise(new ApplicationPollAfter(this));
        }

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Stop();
                _timer.Dispose();
            }

            _disposed = true;
        }

        #endregion IDisposable
    }
}
