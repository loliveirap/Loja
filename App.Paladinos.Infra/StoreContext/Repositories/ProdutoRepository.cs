using Dapper;
using System;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Infra.StoreContext.DataContexts;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Queries.Produto;

namespace App.Paladinos.Infra.StoreContext.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;
        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Alterar(Guid id, Produto produto)
        {
            await _context.Connection.ExecuteAsync(
                @"UPDATE Produto 
                  SET   [Pro-Sku] = @Sku,
                        [Pro-Titulo] = @Titulo, 
                        [Pro-Descricao] = @Descricao,
                        [Pro-Image] = @Image,
                        [Pro-Preco] = @Preco,
                        [Pro-Estoque] = @Estoque
	              WHERE [Pro-GuId] = @Guid",
                new
                {
                    Guid = id,
                    Sku = produto.Sku,
                    Titulo = produto.Titulo,
                    Descricao = produto.Descricao,
                    Image = produto.Image,
                    Preco = produto.Preco,
                    Estoque = produto.Estoque
                });
        }

        public async Task Cadastrar(Produto produto)
        {
            await _context.Connection.ExecuteAsync(
                @"INSERT INTO Produto 
	                      ([Pro-GuId],
                           [Pro-Sku],
                           [Pro-Titulo], 
                           [Pro-Descricao],
                           [Pro-Image],
                           [Pro-Preco],
                           [Pro-Estoque])
	              VALUES  (@Guid, 
		                   @Sku, 
		                   @Titulo, 
		                   @Descricao, 
		                   @Image, 
		                   @Preco, 
		                   @Estoque)",
                new
                {
                    Guid = produto.Guid,
                    Sku = produto.Sku,
                    Titulo = produto.Titulo,
                    Descricao = produto.Descricao,
                    Image = produto.Image,
                    Preco = produto.Preco,
                    Estoque = produto.Estoque
                });
        }

        public async Task<bool> CheckSku(string sku)
        {
            return await _context
                .Connection
                .QueryFirstAsync<bool>(
                @"SELECT CASE WHEN EXISTS (
                    SELECT [Pro-Sku]
                    FROM  Produto
                    WHERE [Pro-Sku] = @Sku
                    )
                    THEN CAST(1 AS BIT)
	                ELSE CAST(0 AS BIT) 
	                END",
                new { Sku = sku });
        }

        public async Task<GetProdutoQueryResult> Get(Guid id)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<GetProdutoQueryResult>(
            @"Select [Pro-GuId] as Id,
	                 [Pro-Sku]  as Sku,
                     [Pro-Titulo] as Titulo, 
                     [Pro-Descricao] as Descricao, 
                     [Pro-Image] as Image, 
                     [Pro-Preco] as Preco, 
                     [Pro-Estoque] as Estoque
               from  Produto 
               WHERE [Pro-GuId] = @Guid",
            new { Guid = id });
        }

        public async Task Remover(Guid id)
        {
            await _context.Connection.ExecuteAsync(
                @"DELETE Produto                   
	              WHERE  [Pro-GuId] = @Guid",
                new { Guid = id });
        }
    }
}
