using Probation.Processes.Configurations;

namespace Probation.Processes.States.Repositories
{
    internal interface IProcessStateRepository
    {
        IProcessState GetState(IProcessConfiguration processConfiguration);
    }
}
