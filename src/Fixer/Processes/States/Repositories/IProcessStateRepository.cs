using Fixer.Processes.Configurations;

namespace Fixer.Processes.States.Repositories
{
    internal interface IProcessStateRepository
    {
        IProcessState GetState(IProcessConfiguration processConfiguration);
    }
}
