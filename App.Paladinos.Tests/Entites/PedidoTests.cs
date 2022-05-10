
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Entities.ValueObjects;
using App.Paladinos.Domain.StoreContext.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Paladinos.Tests.Entites
{
    [TestClass]
    public class PedidoTests
    {
        private Produto _mouse;
        private Produto _keyboard;
        private Produto _chair;
        private Produto _monitor;

        private Cliente _cliente;
        private Pedido _pedido;

        public PedidoTests()
        {
            var name = new Nome("Leandro", "Oliveira", "Paladinos LTDA");
            var document = new Documento("06732687050");
            var email = new Email("teste@teste.com");
            var condicaoPagamento = new CondicaoPagamento("Promoção dia dos pais", 30);
            _cliente = new Cliente(name, document, email, "11985599888");
            _pedido = new Pedido(_cliente, condicaoPagamento);

            _mouse = new Produto("TEC-MS-BK-10","Mouse", "Mouse", "mouse.jpg", 100M, 10);
            _keyboard = new Produto("TEC-TC-WR-11","Teclado", "Teclado", "Teclado.jpg", 100M, 10);
            _chair = new Produto("MOV-CD-BL-12", "Cadeira", "Cadeira", "Cadeira.jpg", 100M, 10);
            _monitor = new Produto("TEC-MT-WS-17-11", "Monitor", "Monitor", "Monitor.jpg", 100M, 10);
        }

        // Consigo criar um novo pedido
        [TestMethod]
        public void DeveCriarPedidoQuandoValido()
        {
            Assert.AreEqual(true, _pedido.IsValid);
        }

        // Ao criar o pedido, o status deve ser created
        [TestMethod]
        public void StatusDeveSerCriadoQuandoOPedidoForCriado()
        {
            Assert.AreEqual(EPedidoStatus.Criado, _pedido.Status);
        }

        // Ao adicionar um novo item, a quantidade deve mudar
        [TestMethod]
        public void DeveRetornarDoisQuandoAdicionadosDoisItens()
        {
            _pedido.AddItem(_monitor, 5);
            _pedido.AddItem(_monitor, 5);

            Assert.AreEqual(2, _pedido.Itens.Count);
        }

        // Ao adicionar um novo item, deve subtrair a quantidade do produto
        [TestMethod]
        public void DeveRetornarCincoQuandoAdicionadosCincoItensComprados()
        {
            _pedido.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.Estoque, 5);
        }

        // Ao confirmar o pedido, deve gerar um número
        [TestMethod]
        public void DeveRetornarUmNumeroQuandoPedidoForConfirmado()
        {
            _pedido.Criar();
            Assert.AreNotEqual("", _pedido.Numero);
        }

        // Ao pagar um pedido, o status deve ser Pago
        [TestMethod]
        public void DeveRetornarPagoQuandoPedidoForPago()
        {
            _pedido.Pagar();
            Assert.AreEqual(EPedidoStatus.Pago, _pedido.Status);
        }

        // Dados mais 10 produtos, devem haver duas entregas
        [TestMethod]
        public void FzerDoisEnviosQuandoCompradoDezProdutos()
        {
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);

            _pedido.Enviar();
            Assert.AreEqual(2, _pedido.Entregas.Count);
        }

        // Ao cancelar o pedido, o status deve ser Cancelado
        [TestMethod]
        public void StatusDeveSerCanceladoQuandoPedidoForCancelado()
        {
            _pedido.Cancelar();
            Assert.AreEqual(EPedidoStatus.Canceledo, _pedido.Status);
        }

        // Ao cancelar o pedido, deve cancelar as entregas
        [TestMethod]
        public void DeveCancelarOsEnviosQuandoPedidoForCancelado()
        {
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);
            _pedido.AddItem(_monitor, 1);

            _pedido.Enviar();
            _pedido.Cancelar();

            foreach (var x in _pedido.Entregas)
            {
                Assert.AreEqual(EEntregaStatus.Cancelado, x.Status);
            }
        }
    }
}
