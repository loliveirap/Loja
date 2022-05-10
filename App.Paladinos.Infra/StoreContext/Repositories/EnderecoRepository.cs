using System;
using Dapper;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Infra.StoreContext.DataContexts;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Queries.Endereco;
using App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input;

namespace App.Paladinos.Infra.StoreContext.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataContext _context;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Alterar(Guid id, Endereco endereco)
        {
            await _context.Connection.ExecuteAsync(
                @"UPDATE Endereco 
                  SET   [End-Rua] = @Rua,
                        [End-Numero] = @Numero, 
                        [End-Complemento] = @Complemento,
                        [End-Municipio] = @Municipio,
                        [End-Estado] = @Estado,
                        [End-Pais] = @Pais,
                        [End-Cep] = @Cep,
                        [End-Tipo] = @Tipo
	              WHERE [End-GuId] = @Guid",
                new
                {
                    Guid = id,
                    Rua = endereco.Rua,
                    Numero = endereco.Numero,
                    Complemento = endereco.Complemento,
                    Municipio = endereco.Municipio,
                    Estado = endereco.Estado,
                    Pais = endereco.Pais,
                    Cep = endereco.Cep,
                    tipo = endereco.Tipo
                });
        }

        public async Task Cadastrar(CreateEnderecoCommand endereco)
        {
            await _context.Connection.ExecuteAsync(
                @"INSERT INTO Endereco 
	                      ([End-GuId], 
		                   [Cli-GuId], 
		                   [End-Rua], 
		                   [End-Numero], 
		                   [End-Complemento], 
		                   [End-Municipio], 
		                   [End-Estado],
                           [End-Pais],
                           [End-Cep],
                           [End-Tipo])
	              VALUES  (@Guid, 
		                   @GuidCliente, 
		                   @Rua, 
		                   @Numero, 
		                   @Complemento, 
		                   @Municipio, 
		                   @Estado,
                           @Pais,
                           @Cep,
                           @Tipo)",
                new
                {
                    Guid = endereco.Guid,
                    GuidCliente = endereco.IdCliente,
                    Rua = endereco.Rua,
                    Numero = endereco.Numero,
                    Complemento = endereco.Complemento,
                    Municipio = endereco.Municipio,
                    Estado = endereco.Estado,
                    Pais = endereco.Pais,
                    Cep = endereco.Cep,
                    Tipo = endereco.Tipo
                });
        }

        public async Task<GetEnderecoQueryResult> Get(Guid id)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<GetEnderecoQueryResult>(
            @"Select [End-GuId] as Id, 
                     [Cli-GuId] as IdCliente,
                     [End-Rua] as Rua,
                     [End-Numero] as Numero, 
                     [End-Complemento] as Complemento,
                     [End-Municipio] as Municipio,
                     [End-Estado] as Estado,
                     [End-Pais] as Pais,
                     [End-Cep] as Cep,
                     [End-Tipo] as Tipo
               from  Endereco 
               WHERE [End-GuId] = @Guid",
            new { Guid = id });
        }

        public async Task Remover(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                @"DELETE Endereco                   
	              WHERE  [End-GuId] = @Guid",
                new { Guid = id });
        }
    }
}
