using FluentValidator;
using App.Paladinos.Shared.Commands;
using FluentValidator.Validation;

namespace App.Paladinos.Domain.StoreContext.Commands.CondicaoPagamentoCommands.Input
{
    public class AlterCondicaoPagamentoCommand : Notifiable, ICommand
    {
        public string Descricao { get; set; }
        public int Dias { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
               .Requires()
               .HasMinLen(Descricao, 3, "Descricao", "A descricao deve conter pelo menos 3 caracteres")
               .HasMaxLen(Descricao, 50, "Descricao", "A descricao deve conter no máximo 50 caracteres")
               .IsNull(Dias, "Dias", "Campo obrigatório")
               );
            return IsValid;
        }
    }
}
