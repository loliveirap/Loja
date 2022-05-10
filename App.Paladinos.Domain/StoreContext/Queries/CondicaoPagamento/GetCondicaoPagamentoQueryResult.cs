using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Queries.CondicaoPagamento
{
    public class GetCondicaoPagamentoQueryResult
    {
        public Guid Id { get; set; }
        public string Descricao { get; private set; }
        public int Dias { get; private set; }
    }
}
