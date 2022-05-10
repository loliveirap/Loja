
using System;
using System.Collections.Generic;
using App.Paladinos.Domain.StoreContext.Queries.Pedido;

namespace App.Paladinos.Domain.StoreContext.Queries.Cliente
{
    public class GetClienteQueryResult
    {
        public GetClienteQueryResult()
        {
            Pedidos = new List<ListClientePedidosQueryResult>();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<ListClientePedidosQueryResult> Pedidos { get; set; }
    }
}
