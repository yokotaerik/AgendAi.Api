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
            // Regular expression to validate the CNPJ format
            var regex = new Regex(@"^\d{14}$");
            if (!regex.IsMatch(cnpj))
                return false;

            // CNPJ validation calculations
            var numbers = cnpj.Substring(0, 12);
            var digits = cnpj.Substring(12, 2);

            var digit1 = CalculateDigit(numbers, 5, 6);
            var digit2 = CalculateDigit(numbers + digit1, 6, 7);

            return digits == digit1 + digit2;
        }

        private static string CalculateDigit(string numbers, int weight1, int weight2)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += (numbers[i] - '0') * (weight1 > 1 ? weight1-- : weight2--);
            }

            int modulo = sum % 11;
            if (modulo < 2)
                return "0";

            return (11 - modulo).ToString();
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
