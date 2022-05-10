
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.Cliente;

namespace App.Paladinos.Domain.StoreContext.Repositories
{
    public interface IClienteRepository
    {
        Task<bool> CheckCnpj(string cnpj);
        Task<bool> CheckEmail(string email);
        Task Salvar(Cliente cliente);
        Task<IEnumerable<ListClienteQueryResult>> Get();
        Task<GetClienteQueryResult> Get(Guid id);
        Task<Cliente> GetById(Guid id);
        Task<IEnumerable<ListClientePedidosQueryResult>> GetPedido(Guid id);
        Task Alterar(Guid id, Cliente cliente);
        Task<IEnumerable<ListClienteEnderecosQueryResult>> GetEnderecos(Guid id);
        Task Remover(Guid id);
        Task RemoverEnderecos(Guid id);
        Task<GetClienteQueryResult> GetByCnpj(string cnpj);
    }
}
