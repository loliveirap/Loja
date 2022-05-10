using System;

namespace App.Paladinos.Domain.StoreContext.Queries.Cliente
{
    public class ListClienteQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
    }
}
