
using App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input;
using App.Paladinos.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System.Collections.Generic;

namespace App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input
{
    public class CreateClienteCommand : Notifiable, ICommand
    {
        public CreateClienteCommand()
        {
            Enderecos = new List<ClienteEnderecoCommand>();
         
        }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<ClienteEnderecoCommand> Enderecos { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(PrimeiroNome, 3, "PrimeiroNome", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(PrimeiroNome, 40, "PrimeiroNome", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(UltimoNome, 3, "UltimoNome", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(UltimoNome, 40, "UltimoNome", "O sobrenome deve conter no máximo 40 caracteres")
                .HasMinLen(RazaoSocial, 3, "RazaoSocial", "A razao social deve conter pelo menos 3 caracteres")
                .HasMaxLen(RazaoSocial, 40, "RazaoSocial", "A razao social deve conter no máximo 40 caracteres")
                .HasMinLen(Cnpj, 14, "Cnpj", "CNPJ inválido")
                .HasMaxLen(Cnpj, 18, "Cnpj", "CNPJ inválido")
                .IsEmail(Email, "Email", "O E-mail é inválido")
            );
            return IsValid;
        }
    }
}