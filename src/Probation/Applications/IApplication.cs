using System;

namespace Probation.Applications
{
    internal interface IApplication : IDisposable
    {
        void Start();
        void Stop();
    }
}
