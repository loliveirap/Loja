using System;
using System.Text;
using System.Threading.Tasks;
using App.Paladinos.Domain.StoreContext.Entities;
using App.Paladinos.Domain.StoreContext.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace App.Paladinos.Infra.StoreContext.Services
{
    public class NotificacaoServico : INotificacaoServices
    {
        public async Task AlteracaoPrecoRabbitMQ(Produto produto)
        {
            string _queue = "NotificaClientesAlteracaoDePreco";

            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = JsonConvert.SerializeObject(produto);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _queue,
                        basicProperties: null,
                        body: body);
                }
            }
            catch (Exception)
            {
               return;
            }
            
        }
    }
}
