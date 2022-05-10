
using System;
using System.Linq;
using FluentValidator;
using System.Threading.Tasks;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Services;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Entities.ValueObjects;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input;
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Output;

namespace App.Paladinos.Domain.StoreContext.Commands.Handlers
{
    public class ClienteHandler :
        Notifiable,
        ICommandHandler<CreateClienteCommand>,
        ICommandHandlerAlter<AlterClienteCommand>,
        ICommandHandlerRemove<RemoveClienteCommand>
    {
        private readonly IClienteRepository _repository;
        private readonly IEmailService _emailService;
        public ClienteHandler(IClienteRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public async Task<ICommandResult> Handle(CreateClienteCommand command)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            // Criar os VOs
            var name = new Nome(command.PrimeiroNome, command.UltimoNome, command.RazaoSocial);
            var document = new Documento(command.Cnpj);
            var email = new Email(command.Email);

            // Criar a entidade
            var cliente = new Cliente(name, document, email, command.Telefone);

            if (command.Enderecos.Count() > 0)
            {
                foreach (var item in command.Enderecos)
                {
                    item.Valid();
                    if (Invalid)
                        return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

                    cliente.AddEndereco(
                        new Endereco(
                            item.Rua,
                            item.Numero,
                            item.Complemento,
                            item.Municipio,
                            item.Estado,
                            item.Pais,
                            item.Cep,
                            item.Tipo
                        ));
                }
            }


            // Validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(cliente.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            // Veriricar se o CNPJ existe n base
            if (await _repository.CheckCnpj(command.Cnpj))
                AddNotification("Documento", "Este CNPJ já está em uso");

            // Verificar se o e-mail existe na base
            if (await _repository.CheckEmail(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            // Persistir o Cliente
            await _repository.Salvar(cliente);

            // Enviar o e-mail de boas vindas
            _emailService.Send( "noreplaypaladinos@gmail.com", email.Address, "Bem vindo", "Seja bem vindo App Paladinos");

            // Retornar o resultado para a tela
            return new CommandResult(true, "Bem vindo ao App Paladinos", new
            {
                Guid = cliente.Guid,
                Nome = name.ToString(),
                Email = email.Address
            });
        }

        public async Task<ICommandResult> Handle(AlterClienteCommand command, Guid id)
        {
            command.Valid();
            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            var dadoscliente = await _repository.Get(id);

            if (dadoscliente == null)
                return new CommandResult(false, "Cliente não encontrado", Notifications);

            // Criar os VOs
            var name = new Nome(command.Nome, command.Sobrenome, command.RazaoSocial);
            var document = new Documento(command.Cnpj);
            var email = new Email(command.Email);

            // Criar a entidade
            var cliente = new Cliente(name, document, email, command.Telefone);

            // Validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(cliente.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corriga os campos abaixo", Notifications);

            await _repository.Alterar(id, cliente);

            return new CommandResult(true, "Cliente alterado com sucesso",
                new
                {
                    Guid = id,
                    Nome = command.ToString(),
                    RazaoSocial = command.RazaoSocial,
                    Email = command.Email
                });
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            // Verifica se o cliente existe
            var cliente = await _repository.Get(id);

            if (cliente == null)
                return new CommandResult(true, "Cliente não encontrado", null);

            // Verifica se o Cliente possui Pedidos
            var pedidos = await _repository.GetPedido(id);

            if (pedidos.Count() > 0)
                return new CommandResult(true, "Não foi possivel remover o cliente, verifique seus pedidos",
                    new
                    {
                        Guid = id
                    });

            // Verifica se o Cliente possui endereços
            var enderecos = await _repository.GetEnderecos(id);

            // Remove endereços
            foreach (var item in enderecos)
            {
                await _repository.RemoverEnderecos(id);
            }

            // Remove Cliente
            await _repository.Remover(id);

            return new CommandResult(true, "Cliente removido com sucesso", null);
        }
    }
}
