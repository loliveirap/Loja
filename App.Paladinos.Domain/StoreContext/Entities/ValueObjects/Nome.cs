using FluentValidator;
using FluentValidator.Validation;

namespace App.Paladinos.Domain.StoreContext.Entities.ValueObjects
{
    public class Nome : Notifiable
    {
        public Nome(string primeiroNome, string ultimoNome, string razaoSocial)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            RazaoSocial = razaoSocial;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(PrimeiroNome, 3, "PrimeiroNome", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(PrimeiroNome, 40, "PrimeiroNome", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(UltimoNome, 3, "UltimoNome", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(UltimoNome, 40, "UltimoNome", "O sobrenome deve conter no máximo 40 caracteres")
                .HasMinLen(RazaoSocial, 3, "RazaoSocial", "A razão social deve conter pelo menos 3 caracteres")
                .HasMaxLen(RazaoSocial, 45, "RazaoSocial", "O razão social deve conter no máximo 40 caracteres")
                );
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public string RazaoSocial { get; private set; }

        public override string ToString()
        {
            return $"{PrimeiroNome} {UltimoNome}";
        }
    }
}
