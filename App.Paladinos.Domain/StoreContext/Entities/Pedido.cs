using App.Paladinos.Domain.StoreContext.Enums;
using App.Paladinos.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class Pedido : Entity
    {
        private readonly IList<ItemPedido> _itens;
        private readonly IList<Entrega> _entregas;

        public Pedido(Cliente cliente, CondicaoPagamento condicaoPagamento)
        {
            Cliente = cliente;
            DateCriacao = DateTime.Now;
            Status = EPedidoStatus.Criado;
            _itens = new List<ItemPedido>();
            _entregas = new List<Entrega>();
            CondicaoPagamento = condicaoPagamento;
        }
        public Cliente Cliente { get; private set; }
        public string Numero { get; private set; }
        public DateTime DateCriacao { get; private set; }
        public EPedidoStatus Status { get; private set; }
        public IReadOnlyCollection<ItemPedido> Itens => _itens.ToArray();
        public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();
        public CondicaoPagamento CondicaoPagamento { get; set; }


        public void AddItem(Produto produto, decimal quantidade)
        {
            if (quantidade > produto.Estoque)
                AddNotification("ItemPedido", $"Produto {produto.Titulo} não tem {quantidade} itens em estoque.");

            var item = new ItemPedido(produto, quantidade);
            _itens.Add(item);
        }

        //Criar um pedido
        public void Criar()
        {
            //Gera o Numero do pedido
            Numero = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

            //Validar
            if (_itens.Count == 0)
                AddNotification("Pedido", "Este pedido não possui itens");
        }

        //Pagar um pedido
        public void Pagar()
        {
            Status = EPedidoStatus.Pago;
        }

        //Enviar um pedido
        public void Enviar()
        {
            // A cadas 5 produtos é uma entrega
            var entregas = new List<Entrega>();
            //deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
            var count = 1;

            //Quebra as entregas
            foreach (var item in _itens)
            {
                if (count == 5)
                {
                    count = 1;
                    entregas.Add(new Entrega(DateTime.Now.AddDays(5)));
                }
                count++;
            }

            //Envia todas as entregas
            entregas.ForEach(x => x.Enviar());

            //Adiciona as entregas ao pedido
            entregas.ForEach(x => _entregas.Add(x));
        }

        //Cancelar um pedido
        public void Cancelar()
        {
            Status = EPedidoStatus.Canceledo;
            _entregas.ToList().ForEach(x => x.Cancelar());
        }
    }
}
