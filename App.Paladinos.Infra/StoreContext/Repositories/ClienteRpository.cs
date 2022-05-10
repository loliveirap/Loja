using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Infra.StoreContext.DataContexts;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Queries.Cliente;
using App.Paladinos.Domain.StoreContext.Entities.ValueObjects;
using System.Linq;

namespace App.Paladinos.Infra.StoreContext.Repositories
{
    public class ClienteRpository : IClienteRepository
    {
        private readonly DataContext _context;
        private readonly IPedidoRepository _pedidoRepository;
        public ClienteRpository(DataContext context, IPedidoRepository pedidoRepository)
        {
            _context = context;
            _pedidoRepository = pedidoRepository;
        }

        public async Task Salvar(Cliente cliente)
        {
            await _context.Connection.ExecuteAsync(
                @"INSERT INTO Cliente 
	                      ([Cli-GuId], 
		                   [Cli-Nome], 
		                   [Cli-Sobrenome], 
		                   [Cli-RazaoSocial], 
		                   [Cli-Cnpj], 
		                   [Cli-Email], 
		                   [Cli-Telefone])
	              VALUES  (@Guid, 
		                   @Nome, 
		                   @Sobrenome, 
		                   @RazaoSocial, 
		                   @Cnpj, 
		                   @Email, 
		                   @Telefone)",
                new
                {
                    Guid = cliente.Guid,
                    Nome = cliente.Nome.PrimeiroNome,
                    Sobrenome = cliente.Nome.UltimoNome,
                    RazaoSocial = cliente.Nome.RazaoSocial,
                    Cnpj = cliente.Cnpj.Numero,
                    Email = cliente.Email.Address,
                    Telefone = cliente.Telefone
                });

            foreach (var endereco in cliente.Enderecos)
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
                    GuidCliente = cliente.Guid,
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
        }

        public async Task<GetClienteQueryResult> Get(Guid id)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<GetClienteQueryResult>(
            @"Select [Cli-GuId] as Id, 
                     [Cli-Nome] + ' ' + [Cli-Sobrenome] as Nome,
                     [Cli-Cnpj] as Cnpj, 
                     [Cli-RazaoSocial] as RazaoSocial,
                     [Cli-Email] as Email
               from  Cliente 
               WHERE [Cli-GuId] = @Guid",
            new { Guid = id });
        }

        public async Task Alterar(Guid id, Cliente cliente)
        {
            await _context.Connection.ExecuteAsync(
                @"UPDATE Cliente 
                  SET   [Cli-Nome] = @Nome, 
		                [Cli-Sobrenome] = @Sobrenome, 
		                [Cli-RazaoSocial] = @RazaoSocial, 
		                [Cli-Cnpj] = @Cnpj, 
		                [Cli-Email] = @Email, 
		                [Cli-Telefone] = @Telefone
	              WHERE [Cli-GuId] = @Guid",
                new
                {
                    Guid = id,
                    Nome = cliente.Nome.PrimeiroNome,
                    Sobrenome = cliente.Nome.UltimoNome,
                    RazaoSocial = cliente.Nome.RazaoSocial,
                    Cnpj = cliente.Cnpj.Numero,
                    Email = cliente.Email.Address,
                    Telefone = cliente.Telefone
                });
        }

        public async Task<bool> CheckCnpj(string cnpj)
        {
            return await _context
                .Connection
                .QueryFirstAsync<bool>(
                @"SELECT CASE WHEN EXISTS (
                    SELECT [Cli-GuId]
                    FROM  Cliente
                    WHERE [Cli-Cnpj] = @Cnpj
                    )
                    THEN CAST(1 AS BIT)
	                ELSE CAST(0 AS BIT) 
	                END",
                new { Cnpj = cnpj });
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await _context
                .Connection
                .QueryFirstAsync<bool>(
                @"SELECT CASE WHEN EXISTS (
                    SELECT [Cli-GuId]
                    FROM  Cliente
                    WHERE [Cli-Email] = @Email
                    )
                    THEN CAST(1 AS BIT)
	                ELSE CAST(0 AS BIT) 
	                END",
                new { Email = email });
        }

        public async Task<IEnumerable<ListClienteQueryResult>> Get()
        {
            return await _context
                .Connection
                .QueryAsync<ListClienteQueryResult>(
                @"Select [Cli-GuId] as Id, 
                         [Cli-Nome] + ' ' + [Cli-Sobrenome] as Nome,
                         [Cli-Cnpj] as Cnpj, 
                         [Cli-Email] 
                  from Cliente");
        }

        public async Task<IEnumerable<ListClientePedidosQueryResult>> GetPedido(Guid id)
        {
            return await _context
                .Connection
                .QueryAsync<ListClientePedidosQueryResult>(
                    @"Select [Cli-GuId] as Id,
                             [Ped-GuId] as IdPedido,
                             [Ped-Numero] as Numero,
                             [Ped-DataCriacao] as DataCriacao,
                             [Ped-Status] as Status
                      from Pedido
                      where [Cli-GuId] = @Id",
                new { id });
        }

        public async Task<IEnumerable<ListClienteEnderecosQueryResult>> GetEnderecos(Guid id)
        {
            return await _context
                .Connection
                .QueryAsync<ListClienteEnderecosQueryResult>(
                    @"Select [End-Id] as IdEndereco
                      from  Endereco
                      where [Cli-GuId] = @Id",
                new { Id = id });
        }

        public async Task Remover(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                    @"DELETE Cliente                   
	                  WHERE [Cli-GuId] = @Guid",
                    new { Guid = id });
        }

        public async Task RemoverEnderecos(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                    @"DELETE Endereco                   
	                  WHERE [Cli-GuId] = @Guid",
                    new { Guid = id });
        }

        public async Task<Cliente> GetById(Guid id)
        {
            var dados = await _context.Connection.QuerySingleOrDefaultAsync<dynamic>(
                                 @"Select [Cli-GuId] as Guid,
                                         [Cli-Nome] as PrimeiroNome,
                                         [Cli-Sobrenome] as UltimoNome,
                                         [Cli-RazaoSocial] as RazaoSocial,
                                         [Cli-Cnpj] as Cnpj,                      
                                         [Cli-Email] as Email,
                                         [Cli-Telefone] as Telefone
                                   from  Cliente 
                                   WHERE [Cli-GuId] = @Guid",
                                    new { Guid = id });

            var nome = new Nome(dados.PrimeiroNome, dados.UltimoNome, dados.RazaoSocial);
            var documento = new Documento(dados.Cnpj);
            var email = new Email(dados.Email);
            var cliente = new Cliente(nome, documento, email, dados.Telefone);
            cliente.Guid = dados.Guid;

            return cliente;
        }

        public async Task<GetClienteQueryResult> GetByCnpj(string cnpj)
        {
            var result = await _context.Connection.QueryFirstOrDefaultAsync<GetClienteQueryResult>(
                                 @"Select [Cli-GuId] as Id,
                                         [Cli-Nome] + ' ' + [Cli-Sobrenome] as Nome,
                                         [Cli-RazaoSocial] as RazaoSocial,
                                         [Cli-Cnpj] as Cnpj,                      
                                         [Cli-Email] as Email,
                                         [Cli-Telefone] as Telefone
                                   from  Cliente 
                                   WHERE [Cli-Cnpj] = @Cnpj",
                                    new { Cnpj = cnpj });

            result.Pedidos = await GetPedido(result.Id);

            foreach (var item in result.Pedidos)
            {
                item.Itens = await _pedidoRepository.GetItensById(item.IdPedido);
            }

            return result;
        }
    }
}
