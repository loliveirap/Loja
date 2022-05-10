using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Paladinos.Shared.Commands
{
    public interface ICommandHandlerRemove<T> where T : ICommand
    {
        Task<ICommandResult> Handle(Guid id);
    }
}
