using System;
using FluentValidator;
using System.Threading.Tasks;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Output;
using App.Paladinos.Domain.StoreContext.Commands.CondicaoPagamentoCommands.Input;

namespace App.Paladinos.Domain.StoreContext.Commands.Handlers
{
    
    public class CondicaoPagamentoHandler :
        Notifiable,
        ICommandHandler<CreateCondicaoPagamentoCommand>,
        ICommandHandlerAlter<AlterCondicaoPagamentoCommand>,
        ICommandHandlerRemove<RemoveCondicaoPagamentoCommand>
    {
        private readonly ICondicaoPagamentoRepository _repository;

        public CondicaoPagamentoHandler(ICondicaoPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(CreateCondicaoPagamentoCommand command)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var condicaoPagamento = new CondicaoPagamento(command.Descricao, command.Dias);

            await _repository.Cadastrar(condicaoPagamento);

            return new CommandResult(true, "Condição de pagamento cadastrado com sucesso",
                new
                {
                    Guid = condicaoPagamento.Guid,
                    Descricao = condicaoPagamento.Descricao,
                    Dias = condicaoPagamento.Dias
                });
        }

        public async Task<ICommandResult> Handle(AlterCondicaoPagamentoCommand command, Guid id)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var dadosCondicaoPagamento = await _repository.GetById(id);

            if (dadosCondicaoPagamento == null)
                return new CommandResult(false, "Condição de pagamento não encontrado", Notifications);

            var condicaoPagamento = new CondicaoPagamento(command.Descricao, command.Dias);

            await _repository.Alterar(id, condicaoPagamento);

            return new CommandResult(true, "Condição de pagamento alterado com sucesso", null);

        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            // Verifica se a Condição de pagamento existe
            var condicaoPagamento = await _repository.GetById(id);

            if (condicaoPagamento == null)
                return new CommandResult(true, "Condição de pagamento não encontrado", null);

            // Remove a Condicao de Pagamento
            await _repository.Remover(id);

            return new CommandResult(true, "Condição de pagamento removido com sucesso", null);
        }
    }
}
