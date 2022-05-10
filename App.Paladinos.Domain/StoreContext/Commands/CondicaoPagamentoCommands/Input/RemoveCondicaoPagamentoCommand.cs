using App.Paladinos.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Commands.CondicaoPagamentoCommands.Input
{
    public class RemoveCondicaoPagamentoCommand : Notifiable, ICommand
    {
        public bool Valid()
        {
            return IsValid;
        }
    }
}
