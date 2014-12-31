using System;

namespace Fixer.Applications
{
    internal interface IApplication : IDisposable
    {
        void Start();
        void Stop();
    }
}
