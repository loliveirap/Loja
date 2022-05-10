
using App.Paladinos.Domain.StoreContext.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Paladinos.Tests.ValueObjects
{
    [TestClass]
    public class DocumentoTests
    {
        private readonly Documento documentoValido;
        private readonly Documento documentoInvalido;

        public DocumentoTests()
        {
            documentoValido = new Documento("87.851.059/0001-71");
            documentoInvalido = new Documento("12345678910");
        }

        [TestMethod]
        public void VerificaNotificaoSeODocumentoEValido()
        {
            Assert.AreEqual(true, documentoValido.IsValid);
            Assert.AreEqual(0, documentoValido.Notifications.Count);
        }

        [TestMethod]
        public void VerificaNotificaoSeODocumentoEInvalido()
        {
            Assert.AreEqual(false, documentoInvalido.IsValid);
            Assert.AreEqual(1, documentoInvalido.Notifications.Count);
        }
    }
}
