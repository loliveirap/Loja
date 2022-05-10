using System;
using FluentValidator;
using System.Threading.Tasks;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Output;
using App.Paladinos.Domain.StoreContext.Commands.ProdutoCommands.Input;
using App.Paladinos.Domain.StoreContext.Services;

namespace App.Paladinos.Domain.StoreContext.Commands.Handlers
{
    public class ProdutoHandler :
        Notifiable,
        ICommandHandler<CreateProdutoCommand>,
        ICommandHandlerAlter<AlterProdutoCommand>,
        ICommandHandlerRemove<RemoveProdutoCommand>
    {
        private readonly IProdutoRepository _repository;
        private readonly INotificacaoServices _notificacaoService;
        public ProdutoHandler(IProdutoRepository repository, INotificacaoServices notificacaoService)
        {
            _repository = repository;
            _notificacaoService = notificacaoService;
        }


        public async Task<ICommandResult> Handle(CreateProdutoCommand command)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            //Validar se o SKU já existe
            if (await _repository.CheckSku(command.Sku))
                AddNotification("Sku", "Este sku já está em uso");

            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var produto =
                new Produto
                (command.Sku,
                command.Titulo,
                command.Descricao,
                command.Image,
                command.Preco,
                command.Estoque);

            await _repository.Cadastrar(produto);

            return new CommandResult(true, "Produto cadastrado com sucesso",
                new
                {
                    Guid = produto.Guid,
                    Nome = produto.ToString(),
                });
        }

        public async Task<ICommandResult> Handle(AlterProdutoCommand command, Guid id)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var dadosProduto = await _repository.Get(id);

            if (dadosProduto == null)
                return new CommandResult(false, "Produto não encontrado", Notifications);

            var produto =
                new Produto
                (command.Sku,
                command.Titulo,
                command.Descricao,
                command.Image,
                command.Preco,
                command.Estoque);

            await _repository.Alterar(id, produto);

            // Envia notificação para a fila do RabbitMQ
            await _notificacaoService.AlteracaoPrecoRabbitMQ(produto);

            return new CommandResult(true, "Produto alterado com sucesso", null);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            // Verifica se o Produto existe
            var produto = await _repository.Get(id);

            if (produto == null)
                return new CommandResult(true, "Produto não encontrado", null);

            // Remove Produto
            await _repository.Remover(id);

            return new CommandResult(true, "Produto removido com sucesso", null);
        }
    }
}
