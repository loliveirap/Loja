using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Paladinos.Tests.Commands
{
    public class CreateClienteCommandTests
    {
        [TestMethod]
        public void VerificaSeOComandoEValido()
        {
            var command = new CreateClienteCommand();
            command.PrimeiroNome = "Leandro";
            command.UltimoNome = "Oliveira";
            command.RazaoSocial = "Paladinos LTDA";
            command.Cnpj = "06732687050";
            command.Email = "leandro@teste.com";
            command.Telefone = "11999889966";

            Assert.AreEqual(true, command.Valid());
        }
    }
}
