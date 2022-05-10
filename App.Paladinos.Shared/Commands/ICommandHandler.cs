using System.Threading.Tasks;

namespace App.Paladinos.Shared.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
