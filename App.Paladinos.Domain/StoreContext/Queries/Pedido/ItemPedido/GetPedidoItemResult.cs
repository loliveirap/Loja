using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Queries.Pedido.ItemPedido
{
    public class GetPedidoItemResult
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
