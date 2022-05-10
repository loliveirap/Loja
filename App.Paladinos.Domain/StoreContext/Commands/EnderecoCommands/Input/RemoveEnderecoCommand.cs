
using FluentValidator;
using App.Paladinos.Shared.Commands;

namespace App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input
{
    public class RemoveEnderecoCommand : Notifiable, ICommand
    {
        public bool Valid()
        {
            return true;
        }
    }
}
