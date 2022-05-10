using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.Pedido;
using App.Paladinos.Domain.StoreContext.Queries.Pedido.ItemPedido;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Paladinos.Domain.StoreContext.Repositories
{
    public interface IPedidoRepository
    {
        Task<GetPedidoQueryResult> GetById(Guid id);
        Task <IEnumerable<GetPedidoItemResult>> GetItensById(Guid id);
        Task<bool> Salvar(Pedido pedido);
        Task Alterar(Guid id, Pedido pedido);
        Task Remover(Guid id);
    }
}
