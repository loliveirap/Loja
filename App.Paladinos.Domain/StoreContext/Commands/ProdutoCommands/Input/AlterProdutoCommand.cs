using App.Paladinos.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace App.Paladinos.Domain.StoreContext.Commands.ProdutoCommands.Input
{
    public class AlterProdutoCommand : Notifiable, ICommand
    {
        public string Sku { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Image { get; set; }
        public decimal Preco { get; set; }
        public decimal Estoque { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
               .Requires()
               .IsNotNullOrEmpty(Sku, "Sku", "Campo obrigatório")
               .HasMinLen(Sku, 3, "Sku", "O Sku deve conter pelo menos 3 caracteres")
               .HasMaxLen(Sku, 50, "Sku", "O Sku deve conter no máximo 50 caracteres")
               .HasMinLen(Titulo, 3, "Titulo", "O Titulo deve conter pelo menos 3 caracteres")
               .HasMaxLen(Titulo, 40, "Titulo", "O Titulo deve conter no máximo 40 caracteres")
               .HasMinLen(Descricao, 3, "Descricao", "A descricao deve conter pelo menos 3 caracteres")
               .HasMaxLen(Descricao, 40, "Descricao", "AdDescricao deve conter no máximo 40 caracteres")
               .IsNotNullOrEmpty(Image, "Image", "Campo obrigatório")
               .IsNull(Preco, "Preco", "Campo obrigatório")
               .IsNull(Estoque, "Estoque", "Campo obrigatório")
               );
            return IsValid;
        }
    }
}
