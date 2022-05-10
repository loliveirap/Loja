
using App.Paladinos.Shared.Entities;

namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class ItemPedido : Entity
    {
        public ItemPedido(Produto produto, decimal quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            Preco = produto.Preco;

            if (produto.Estoque < quantidade)
                AddNotification("Quantidade", "Produto fora de estoque");

            produto.RemoveEstoque(quantidade);
        }
        public Produto Produto { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal Preco { get; private set; }
    }
}
