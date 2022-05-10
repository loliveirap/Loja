using App.Paladinos.Shared.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Commands.PedidoCommands.Input
{
    public class RemovePedidoCommand : Notifiable, ICommand
    {
        public bool Valid()
        {
            throw new NotImplementedException();
        }
    }
}
