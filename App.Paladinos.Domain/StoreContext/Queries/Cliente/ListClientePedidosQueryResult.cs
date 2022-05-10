using App.Paladinos.Domain.StoreContext.Queries.Pedido.ItemPedido;
using System;
using System.Collections.Generic;

namespace App.Paladinos.Domain.StoreContext.Queries.Cliente
{
    public class ListClientePedidosQueryResult
    {
        public ListClientePedidosQueryResult()
        {
            Itens = new List<GetPedidoItemResult>();
        }
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public string Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public IEnumerable<GetPedidoItemResult> Itens { get; set; }
    }
}
