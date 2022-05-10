using System;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.Produto;

namespace App.Paladinos.Domain.StoreContext.Repositories
{
    public interface IProdutoRepository
    {
        Task Cadastrar(Produto produto);
        Task<GetProdutoQueryResult> Get(Guid id);
        Task Alterar(Guid id, Produto produto);
        Task Remover(Guid id);
        Task<bool> CheckSku(string sku);
    }
}
