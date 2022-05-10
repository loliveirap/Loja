using FluentValidator;
using App.Paladinos.Shared.Commands;

namespace App.Paladinos.Domain.StoreContext.Commands.ProdutoCommands.Input
{
    public class RemoveProdutoCommand : Notifiable, ICommand
    {
        public bool Valid()
        {
            return IsValid;
        }
    }
}
