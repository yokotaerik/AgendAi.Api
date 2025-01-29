namespace MarcAI.Domain.Models.ValueObjects;

public class Cpf 
{
    public string Value { get; }



#pragma warning disable CS8618
    private Cpf() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private Cpf(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Cpf is required.", nameof(value));

        var sanitizedValue = Sanitize(value);

        if (!IsValid(sanitizedValue))
            throw new ArgumentException("Invalid Cpf.", nameof(value));

        Value = sanitizedValue;
    }

    public static bool IsValid(string Cpf)
    {
        if (Cpf.Length != 11 || !long.TryParse(Cpf, out _))
            return false;

        if (new string(Cpf[0], 11) == Cpf)
            return false;

        return CheckDigits(Cpf);
    }

    private static bool CheckDigits(string Cpf)
    {
        var firstCheckSum = 0;
        for (int i = 0; i < 9; i++)
            firstCheckSum += (Cpf[i] - '0') * (10 - i);

        var firstCheckDigit = (firstCheckSum * 10) % 11;
        if (firstCheckDigit == 10) firstCheckDigit = 0;

        if (firstCheckDigit != Cpf[9] - '0')
            return false;

        var secondCheckSum = 0;
        for (int i = 0; i < 10; i++)
            secondCheckSum += (Cpf[i] - '0') * (11 - i);

        var secondCheckDigit = (secondCheckSum * 10) % 11;
        if (secondCheckDigit == 10) secondCheckDigit = 0;

        return secondCheckDigit == Cpf[10] - '0';
    }

    private static string Sanitize(string Cpf)
    {
        return Cpf.Replace(".", "").Replace("-", "").Trim();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Value.Substring(0, 3)}.{Value.Substring(3, 3)}.{Value.Substring(6, 3)}-{Value.Substring(9, 2)}";
    }

    public static Cpf Create(string value)
    {
        return new Cpf(value);
    }
}
