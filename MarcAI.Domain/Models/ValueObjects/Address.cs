using System.Runtime.ConstrainedExecution;

namespace MarcAI.Domain.Models.ValueObjects;

public class Address
{
    public string Street { get; }
    public string Number { get; }
    public string? Complement { get; }
    public string? Neighborhood { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }


#pragma warning disable CS8618
    private Address() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private Address(string? street, string? number, string? complement, string? neighborhood, string?  city, string? state, string? zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.", nameof(street));
        if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Number is required.", nameof(number));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.", nameof(city));
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required.", nameof(state));
        if (string.IsNullOrWhiteSpace(zipCode) || !IsValidZipCode(zipCode)) throw new ArgumentException("Invalid ZIP code.", nameof(zipCode));

        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    private static bool IsValidZipCode(string zipCode)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^\d{5}-\d{3}$");
    }
  
    public override string ToString()
    {
        return $"{Street}, {Number}{(string.IsNullOrWhiteSpace(Complement) ? "" : $" - {Complement}")}, {Neighborhood}, {City}, {State}, CEP: {ZipCode}";
    }

    public static Address Create(string? street, string? number, string? complement, string? neighborhood, string? city, string? state, string? zipCode)
    {
        return new Address(street, number, complement, neighborhood, city, state, zipCode);
    }
}
