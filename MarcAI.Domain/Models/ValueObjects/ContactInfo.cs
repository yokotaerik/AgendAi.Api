using MarcAI.Domain.Enums.Contact;
using System.Text.RegularExpressions;

namespace MarcAI.Domain.Models.ValueObjects;

public class ContactInfo
{
    public string Info { get; private set; } = null!;
    public ContactType Type { get; private set; }


    private ContactInfo()
    {
    }

    public static ContactInfo CreatePhone(string info)
    {
        if (!IsValidPhoneNumber(info))
            throw new ArgumentException("Invalid phone number format.");

        return new ContactInfo
        {
            Info = info,
            Type = ContactType.CallPhone
        };
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Regex para validar números de celular no formato E.164
        var regex = new Regex(@"^\+?[1-9]\d{1,14}$");
        return regex.IsMatch(phoneNumber);
    }
}
