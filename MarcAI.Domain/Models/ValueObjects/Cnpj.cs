using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MarcAI.Domain.Models.ValueObjects
{
    public class Cnpj
    {
        public string Value { get; private set; }
        private Cnpj(string value)
        {
            Value = value;
        }


#pragma warning disable CS8618
        private Cnpj() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

        public static Cnpj Create(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("CNPJ cannot be null or empty.");

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            if (!IsCnpjValid(cnpj))
                throw new ArgumentException("Invalid CNPJ.");

            return new Cnpj(cnpj);
        }

        private static bool IsCnpjValid(string cnpj)
        {
            if (!Regex.IsMatch(cnpj, @"^\d{14}$"))
                return false;

            int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string numbers = cnpj.Substring(0, 12);
            string digits = cnpj.Substring(12, 2);

            char digit1 = CalculateDigit(numbers, weights1);
            char digit2 = CalculateDigit(numbers + digit1, weights2);

            return digits == $"{digit1}{digit2}";
        }

        private static char CalculateDigit(string numbers, int[] weights)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += (numbers[i] - '0') * weights[i];
            }

            int remainder = sum % 11;
            return remainder < 2 ? '0' : (char)('0' + (11 - remainder));
        }

        public override string ToString()
        {
            return Value;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
