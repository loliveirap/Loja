using FluentValidator;
using System.Threading.Tasks;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.PedidoCommands.Input;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Output;
using System;

namespace App.Paladinos.Domain.StoreContext.Commands.Handlers
{
    public class PedidoHandler :
        Notifiable,
        ICommandHandler<CreatePedidoCommand>,
        ICommandHandlerAlter<AlterPedidoCommand>,
        ICommandHandlerRemove<RemovePedidoCommand>
    {
        private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _repositoryCliente;
        private readonly IProdutoRepository _repositoryProduto;
        private readonly ICondicaoPagamentoRepository _repositoryCondicaoPagamento;

        public PedidoHandler(
            IPedidoRepository repository,
            IClienteRepository repositoryCliente,
            IProdutoRepository repositoryProduto,
            ICondicaoPagamentoRepository repositoryCondicaoPagamento)
        {
            _repository = repository;
            _repositoryCliente = repositoryCliente;
            _repositoryProduto = repositoryProduto;
            _repositoryCondicaoPagamento = repositoryCondicaoPagamento;
        }

        public async Task<ICommandResult> Handle(CreatePedidoCommand command)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            //Obter dados do Cliente
            var cliente = await _repositoryCliente.GetById(command.Cliente);

            //Obter Condicao de Pagamento
            var dadosCondicaoPagamento = await _repositoryCondicaoPagamento.GetById(command.CondicaoPagamento);
            var condicaoPagamento = new CondicaoPagamento(dadosCondicaoPagamento.Descricao, dadosCondicaoPagamento.Dias);
            condicaoPagamento.Guid = command.CondicaoPagamento;

            if (cliente == null)
                AddNotification("Cliente", "Cliente não encontrado");

            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var pedido = new Pedido(cliente, condicaoPagamento);

            foreach (var item in command.ItemPedido)
            {
                // Obter lista de produtos
                var estoque = await _repositoryProduto.Get(item.Produto);

                var produto = new Produto(
                    estoque.Sku,
                    estoque.Titulo,
                    estoque.Descricao,
                    estoque.Image,
                    estoque.Preco,
                    estoque.Estoque
                    );
                produto.Guid = item.Produto;

                // Adicionar Itens do pedido
                pedido.AddItem(produto, item.Quantidade);
            }

            if (Invalid)
            {
                pedido.Cancelar();
                return new CommandResult(false, "Erro ao processar seu pedido", Notifications);
            }


            pedido.Criar();
            if (await _repository.Salvar(pedido))
                return new CommandResult(true, "Pedido cadastrado com sucesso", new { Pedido = pedido.Numero });
            else
            {
                pedido.Cancelar();
                return new CommandResult(false, "Erro ao processar seu pedido", Notifications);
            }
        }

        public Task<ICommandResult> Handle(AlterPedidoCommand command, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICommandResult> Handle(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
