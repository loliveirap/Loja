using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Queries.CondicaoPagamento;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Infra.StoreContext.DataContexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Paladinos.Infra.StoreContext.Repositories
{
    public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
    {
        private readonly DataContext _context;
        public CondicaoPagamentoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Alterar(Guid id, CondicaoPagamento condicaoPagamento)
        {
            await _context.Connection.ExecuteAsync(
                @"UPDATE CondPagto 
                  SET   [Cpg-Descricao] = @Descricao,
                        [Cpg-Dias] = @Dias
	              WHERE [Cpg-GuId] = @Guid",
                new
                {
                    Guid = id,
                    Descricao = condicaoPagamento.Descricao,
                    Dias = condicaoPagamento.Dias
                });
        }

        public async Task Cadastrar(CondicaoPagamento condicaoPagamento)
        {
            await _context.Connection.ExecuteAsync(
                @"INSERT INTO CondPagto 
	                      ([Cpg-GuId],
                           [Cpg-Descricao] ,
                           [Cpg-Dias])
	              VALUES  (@GuId, 
		                   @Descricao, 
		                   @Dias)",
                new
                {
                    Guid = condicaoPagamento.Guid,
                    Descricao = condicaoPagamento.Descricao,
                    Dias = condicaoPagamento.Dias
                });
        }

        public async Task<GetCondicaoPagamentoQueryResult> GetById(Guid id)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<GetCondicaoPagamentoQueryResult>(
            @"Select [Cpg-GuId] as Id,
                     [Cpg-Descricao] as Descricao,
                     [Cpg-Dias] as Dias
               from  CondPagto 
               WHERE [Cpg-GuId] = @Guid",
            new { Guid = id });
        }

        public async Task Remover(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                @"DELETE CondPagto                   
	              WHERE  [Cpg-GuId] = @Guid",
                new { Guid = id });
        }
    }
}
