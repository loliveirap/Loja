using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Paladinos.Shared.Commands
{
    public interface ICommandHandlerAlter<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command, Guid id);
    }
}
