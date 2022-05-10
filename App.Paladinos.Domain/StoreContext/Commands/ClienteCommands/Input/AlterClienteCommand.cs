using App.Paladinos.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input
{
    public class AlterClienteCommand : Notifiable, ICommand
    {        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Nome, 3, "Nome", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(Nome, 40, "Nome", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(Sobrenome, 3, "Sobrenome", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(Sobrenome, 40, "Sobrenome", "O sobrenome deve conter no máximo 40 caracteres")
                .HasMinLen(RazaoSocial, 3, "RazaoSocial", "A razao social deve conter pelo menos 3 caracteres")
                .HasMaxLen(RazaoSocial, 40, "RazaoSocial", "A razao social deve conter no máximo 40 caracteres")
                .HasMinLen(Cnpj, 14, "Cnpj", "CNPJ inválido")
                .HasMaxLen(Cnpj, 18, "Cnpj", "CNPJ inválido")
                .IsEmail(Email, "Email", "O E-mail é inválido")
            );
            return IsValid;
        }
        public override string ToString()
        {
            return $"{Nome} {Sobrenome}";
        }
    }
}
