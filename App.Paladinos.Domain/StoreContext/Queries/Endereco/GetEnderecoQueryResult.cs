using System;
using System.Collections.Generic;
using System.Text;

namespace App.Paladinos.Domain.StoreContext.Queries.Endereco
{
    public class GetEnderecoQueryResult
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Municipio { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        public string Tipo { get; set; }
    }
}
