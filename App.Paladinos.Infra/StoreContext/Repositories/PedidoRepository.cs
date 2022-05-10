using Dapper;
using System;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Infra.StoreContext.DataContexts;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Queries.Pedido;
using App.Paladinos.Domain.StoreContext.Queries.Pedido.ItemPedido;
using System.Collections.Generic;

namespace App.Paladinos.Infra.StoreContext.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataContext _context;
        public PedidoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Alterar(Guid id, Pedido pedido)
        {
            await _context.Connection.ExecuteAsync(
                    @"UPDATE Produto 
                      SET   [Ped-Status] = @Status
	                  WHERE [Pro-GuId] = @Guid",
                    new
                    {
                        Guid = id,
                        Status = pedido.Status
                    });

            // Alterar itens do pedido
        }

        public async Task<GetPedidoQueryResult> GetById(Guid id)
        {
            var dados = await _context.Connection.QuerySingleOrDefaultAsync<dynamic>(
                                @"SELECT [Ped-GuId] as Guid,
                                         [Ped-Numero] as Numero,
                                         [Cli-GuId]] as IdCliente,
                                         [Ped-DataCriacao] as DateCriacao,
                                         [Ped-Status] as Status					 
                                   FROM  Pedido 
                                   WHERE [Ped-GuId] = @Guid",
                                   new { Guid = id }).Result();

            var pedido = new GetPedidoQueryResult
            {
                Id = dados.Guid,
                IdCliente = dados.IdCliente,
                Numero = dados.Numero,
                DateCriacao = dados.DateCriacao,
                Status = dados.Status
            };
            
            var itensPedidos = await GetItensById(pedido.Id );

            pedido.Itens = itensPedidos;
            return pedido;
        }

        public async Task<IEnumerable<GetPedidoItemResult>> GetItensById(Guid id)
        {
            return await _context.Connection.QueryAsync<GetPedidoItemResult>(
                           @"SELECT [Itm-GuId] as Id,
                                     [Ped-GuId] as IdPedido,
                                     [Pro-GuId] as IdProduto,
                                     [Itm-Quantidade] as Quantidade,
                                     [Itm-Preco] as Preco					 
                            FROM  ItemPedido 
                            WHERE [Ped-GuId] = @Guid",
                           new { Guid = id });
        }

        public async Task Remover(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                    @"DELETE Pedido                   
	                  WHERE [Ped-GuId] = @Guid",
                    new { Guid = id });
        }

        public async Task<bool> Salvar(Pedido pedido)
        {
            using (var conexaoTransaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    await _context.Connection.ExecuteAsync(
                            @"INSERT INTO Pedido 
	                                  ([Ped-GuId], 
		                               [Ped-Numero], 
		                               [Cli-GuId],
                                       [Cpg-GuId], 
		                               [Ped-DataCriacao], 
		                               [Ped-Status])
	                          VALUES  (@Guid, 
		                               @Numero, 
		                               @Cliente,
                                       @CondPagto,
		                               @Data, 
		                               @Status)",
                        new
                        {
                            Guid = pedido.Guid,
                            Numero = pedido.Numero,
                            Cliente = pedido.Cliente.Guid,
                            CondPagto = pedido.CondicaoPagamento.Guid,
                            Data = pedido.DateCriacao,
                            Status = pedido.Status
                        }, conexaoTransaction);

                    foreach (var item in pedido.Itens)
                    {
                        await _context.Connection.ExecuteAsync(
                                @"INSERT INTO ItemPedido 
	                                      ([Itm-GuId], 
		                                   [Ped-GuId], 
		                                   [Pro-GuId], 
		                                   [Itm-Quantidade], 
		                                   [Itm-Preco])
	                              VALUES  (@Guid, 
		                                   @Pedido, 
		                                   @Produto, 
		                                   @Quantidade, 
		                                   @Preco)",
                        new
                        {
                            Guid = item.Guid,
                            Pedido = pedido.Guid,
                            Produto = item.Produto.Guid,
                            Quantidade = item.Quantidade,
                            Preco = item.Preco
                        }, conexaoTransaction);

                        // Atualziar Estoque
                        await _context.Connection.ExecuteAsync(
                                @"UPDATE Produto 
                                  SET   [Pro-Estoque] = @Estoque
	                              WHERE [Pro-GuId] = @Guid",
                                new
                                {
                                    Guid = item.Produto.Guid,
                                    Estoque = item.Produto.Estoque
                                }, conexaoTransaction);
                    }

                    conexaoTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    conexaoTransaction.Rollback();
                    return false;
                }
            }
                
        }
    }
}
