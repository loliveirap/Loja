using System;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.Endereco;
using App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input;

namespace App.Paladinos.Domain.StoreContext.Repositories
{
    public interface IEnderecoRepository
    {
        Task Cadastrar(CreateEnderecoCommand endereco);
        Task<GetEnderecoQueryResult> Get(Guid id);
        Task Alterar(Guid id, Endereco endereco);
        Task Remover(Guid id);

    }
}
