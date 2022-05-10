
using App.Paladinos.Domain.StoreContext.Enums;
using App.Paladinos.Domain.StoreContext.Queries.Pedido.ItemPedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Queries.Pedido
{
    public class GetPedidoQueryResult
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string Numero { get; set; }
        public DateTime DateCriacao { get; set; }
        public EPedidoStatus Status { get; set; }
        public IEnumerable<GetPedidoItemResult> Itens { get; set; }
    }
}
