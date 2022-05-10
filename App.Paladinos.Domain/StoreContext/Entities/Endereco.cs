
using App.Paladinos.Domain.StoreContext.Enums;
using App.Paladinos.Shared.Entities;


namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class Endereco : Entity
    {
        public Endereco(
            string rua,
            string numero,
            string complemento,
            string municipio,
            string estado,
            string pais,
            string cep,
            EEnderecoType type
            )
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Municipio = municipio;
            Estado = estado;
            Pais = pais;
            Cep = cep;
            Tipo = type;
        }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Municipio { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        public EEnderecoType Tipo { get; set; }

        public override string ToString()
        {
            return $"{Rua}, {Numero} - {Municipio}/{Estado}";
        }
    }
}
