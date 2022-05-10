using App.Paladinos.Domain.StoreContext.Enums;
using App.Paladinos.Shared.Entities;
using System;

namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class Entrega : Entity
    {
        public Entrega(DateTime estimateDeliveryDate)
        {
            DataCriacao = DateTime.Now;
            DataEntregaEstimada = estimateDeliveryDate;
            Status = EEntregaStatus.Aguardando;
        }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataEntregaEstimada { get; private set; }
        public EEntregaStatus Status { get; private set; }

        public void Enviar()
        {
            // Se data estimada de entrega for no passado, não entregar
            Status = EEntregaStatus.Enviado;
        }

        public void Cancelar()
        {
            //Se o status ja estiver entregue não pode cancelar
            Status = EEntregaStatus.Cancelado;
        }
    }
}
