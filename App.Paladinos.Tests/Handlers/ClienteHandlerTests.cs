
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Paladinos.Tests.Handlers
{
    [TestClass]
    public class ClienteHandlerTests
    {
        [TestMethod]
        public void DeveCadastrarUmClienteSeOComandoForValido()
        {
            var command = new CreateClienteCommand
            {
                PrimeiroNome = "Leandro",
                UltimoNome = "Oliveira",
                RazaoSocial = "Paladinos LTDA",
                Cnpj = "02.683.114/0001-09",
                Email = "leandro@teste.com",
                Telefone = "11999889966"
            };

            var handler = new ClienteHandler(new FakeClienteRepository(), new FakeEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.IsValid);
        }
    }
}
