using App.Paladinos.Shared.Entities;

namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class CondicaoPagamento : Entity
    {
        public CondicaoPagamento(string descricao, int dias)
        {
            Descricao = descricao;
            Dias = dias;
        }

        public string Descricao { get; private set; }
        public int Dias { get; private set; }
    }
}
