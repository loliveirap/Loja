using App.Paladinos.Domain.StoreContext.Services;

namespace App.Paladinos.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body) { }
    }
}
