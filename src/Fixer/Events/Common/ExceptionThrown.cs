using System;

namespace Fixer.Events.Common
{
    internal class ExceptionThrown : IEvent
    {
        public Exception Exception { get; private set; }

        public ExceptionThrown(Exception exception)
        {
            Exception = exception;
        }
    }
}
