using System;
using FluentValidator;
using System.Threading.Tasks;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Output;
using App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input;

namespace App.Paladinos.Domain.StoreContext.Commands.Handlers
{
    public class EnderecoHandler :
        Notifiable,
        ICommandHandler<CreateEnderecoCommand>,
        ICommandHandlerAlter<AlterEnderecoCommand>,
        ICommandHandlerRemove<RemoveEnderecoCommand>
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoHandler(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(CreateEnderecoCommand command)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            await _repository.Cadastrar(command);

            return new CommandResult(true, "Endereço cadastrado com sucesso", null);
        }

        public async Task<ICommandResult> Handle(AlterEnderecoCommand command, Guid id)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var dadosEndereco = await _repository.Get(id);

            if (dadosEndereco == null)
                return new CommandResult(false, "Endereco não encontrado", Notifications);

            var endereco =
                new Endereco(
                    command.Rua,
                    command.Numero,
                    command.Complemento,
                    command.Municipio,
                    command.Estado,
                    command.Pais,
                    command.Cep,
                    command.Tipo
                );

            await _repository.Alterar(id, endereco);

            return new CommandResult(true, "Endereço atualizado com sucesso", null);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            // Verifica se o Endereço existe
            var endereco = await _repository.Get(id);

            if (endereco == null)
                return new CommandResult(true, "Endereço não encontrado", null);

            // Remove Endereço
            await _repository.Remover(id);

            return new CommandResult(true, "Endereço removido com sucesso", null);
        }
    }
}
