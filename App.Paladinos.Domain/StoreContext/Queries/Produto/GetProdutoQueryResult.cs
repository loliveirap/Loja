using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Queries.Produto
{
    public class GetProdutoQueryResult
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Image { get; private set; }
        public decimal Preco { get; private set; }
        public decimal Estoque { get; private set; }
    }
}
