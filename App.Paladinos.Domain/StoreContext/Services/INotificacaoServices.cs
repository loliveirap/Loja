using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;

namespace App.Paladinos.Domain.StoreContext.Services
{
    public interface INotificacaoServices
    {
        Task AlteracaoPrecoRabbitMQ(Produto produto);
    }
}
