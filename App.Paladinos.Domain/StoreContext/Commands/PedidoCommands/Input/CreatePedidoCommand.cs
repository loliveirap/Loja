using System;
using FluentValidator;
using FluentValidator.Validation;
using System.Collections.Generic;
using App.Paladinos.Shared.Commands;


namespace App.Paladinos.Domain.StoreContext.Commands.PedidoCommands.Input
{
    public class CreatePedidoCommand : Notifiable, ICommand
    {
        public CreatePedidoCommand()
        {
            ItemPedido = new List<ItemPedidoCommand>();
        }

        public Guid Cliente { get; set; }
        public Guid CondicaoPagamento { get; set; }
        public IList<ItemPedidoCommand> ItemPedido { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
               .Requires()
               .HasLen(Cliente.ToString(), 30, "Cliente", "Identificador do cliente inválido")
               .IsGreaterThan(ItemPedido.Count, 40, "Itens", "Nenhum item do pedido foi encontrado")
               .IsNull(CondicaoPagamento, "CondicaoPagamento","Campo obrigatório")
           );
            return IsValid;
        }
    }

    public class ItemPedidoCommand
    {
        public Guid Produto { get; set; }
        public decimal Quantidade { get; set; }
    }
}
