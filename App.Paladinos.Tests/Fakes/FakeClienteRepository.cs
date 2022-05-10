using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.Cliente;
using App.Paladinos.Domain.StoreContext.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Paladinos.Tests.Fakes
{
    public class FakeClienteRepository : IClienteRepository
    {
        public Task Alterar(Guid id, Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckCnpj(string documento)
        {
            return false;
        }

        public async Task<bool> CheckEmail(string email)
        {
            return false;
        }

        public async Task<IEnumerable<ListClienteQueryResult>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<GetClienteQueryResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetClienteQueryResult> GetByCnpj(string cnpj)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListClienteEnderecosQueryResult>> GetEnderecos(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ListClientePedidosQueryResult>> GetPedido(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoverEnderecos(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Salvar(Cliente cliente) { }
    }
}
