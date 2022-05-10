using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.CondicaoPagamento;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Paladinos.Domain.StoreContext.Repositories
{
    public interface ICondicaoPagamentoRepository
    {
        Task Cadastrar(CondicaoPagamento condicaoPagamento);
        Task<GetCondicaoPagamentoQueryResult> GetById(Guid id);
        Task Alterar(Guid id, CondicaoPagamento condicaoPagamento);
        Task Remover(Guid id);
    }
}
