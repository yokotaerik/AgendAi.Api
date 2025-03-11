using MarcAI.Domain.Enums.Contact;

namespace MarcAI.Domain.Models.ValueObjects;

public class ContactInfo
{
    public string Info { get; private set; }
    public ContactType Type { get; private set; }
}
