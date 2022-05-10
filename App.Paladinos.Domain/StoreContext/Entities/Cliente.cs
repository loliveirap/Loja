
using App.Paladinos.Domain.StoreContext.Entities.ValueObjects;
using App.Paladinos.Shared.Entities;
using System.Collections.Generic;
using System.Linq;


namespace App.Paladinos.Domain.StoreContext.Entities
{
    public class Cliente : Entity
    {
        private readonly IList<Endereco> _enderecos;
        public Cliente(
            Nome nome,
            Documento documento,
            Email email,
            string telefone)
        {
            Nome = nome;
            Cnpj = documento;
            Email = email;
            Telefone = telefone;
            _enderecos = new List<Endereco>();
        }

        //Prorpiedades
        public Nome Nome { get; private set; }
        public Documento Cnpj { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }
        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.ToArray();

        // Metodos
        public void AddEndereco(Endereco endereco)
        {
            _enderecos.Add(endereco);
        }

        public override string ToString()
        {
            return Nome.ToString();
        }

        // Eventos
    }
}
