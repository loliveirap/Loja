
using App.Paladinos.Domain.StoreContext.Enums;
using App.Paladinos.Shared.Commands;
using App.Paladinos.Shared.Entities;
using FluentValidator;
using FluentValidator.Validation;
using System;

namespace App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input
{
    public class CreateEnderecoCommand : Notifiable, ICommand
    {
        public CreateEnderecoCommand()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; private set; }
        public Guid IdCliente { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public EEnderecoType Tipo { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
               .Requires()
               .HasMinLen(Rua, 3, "Rua", "A rua deve conter pelo menos 3 caracteres")
               .HasMaxLen(Rua, 40, "Rua", "O nome deve conter no máximo 40 caracteres")
               .IsNotNullOrEmpty(Numero, "Numero", "O número é obrigatório")
               .HasMinLen(Municipio, 3, "Municipio", "O municipio deve conter pelo menos 3 caracteres")
               .HasMaxLen(Municipio, 60, "Municipio", "O municipio deve conter no máximo 60 caracteres")
               .HasLen(Estado, 2, "Estado", "O estado deve conter 2 caracteres")
               .HasLen(Pais, 2, "Pais", "O pais deve conter 2 caracteres")
               .HasLen(Cep, 8, "Cep", "O CEP deve conter 8 caracteres, somente números")
               );

            return IsValid;
        }
    }
}
