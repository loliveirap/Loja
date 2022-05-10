using App.Paladinos.Shared.Entities;

namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class Produto : Entity
    {
        public Produto(
            string sku,
            string titulo,
            string descricao,
            string image,
            decimal preco,
            decimal quantidade)
        {
            Sku = sku;
            Titulo = titulo;
            Descricao = descricao;
            Image = image;
            Preco = preco;
            Estoque = quantidade;
        }
        public string Sku { get; set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Image { get; private set; }
        public decimal Preco { get; private set; }
        public decimal Estoque { get; private set; }

        public override string ToString()
        {
            return Titulo;
        }

        public void RemoveEstoque(decimal quantidade)
        {
            Estoque -= quantidade;
        }
    }
}
