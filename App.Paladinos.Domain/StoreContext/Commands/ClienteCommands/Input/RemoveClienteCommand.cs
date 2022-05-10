
using FluentValidator;
using App.Paladinos.Shared.Commands;

namespace App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input
{
    public class RemoveClienteCommand : Notifiable, ICommand
    {
        public bool Valid()
        {
            return true;
        }
    }
}
