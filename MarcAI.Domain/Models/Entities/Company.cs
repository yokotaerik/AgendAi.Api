using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Domain.Models.Entities;

public class Company : BaseEntity
{
    public string CorporateName { get; private set; }
    public string FantasyName { get; private set; }
    public Address Address { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public  ICollection<Employee> Employees { get; private set; } = [];
    public ICollection<Service> Services { get; private set; } = [];
    public ICollection<Photo> Photos { get; private set; } = [];

#pragma warning disable CS8618
    private Company() { } // Supressão do aviso de inicialização
    #pragma warning restore CS8618

    private Company(string corporateName, string fantasyName, Address address, Cnpj cnpj)
    {
        CorporateName = corporateName;
        FantasyName = fantasyName;
        Address = address;
        Cnpj = cnpj;
    }

    public static Company Create(string corporateName, string fantasyName, Address address, Cnpj cnpj)
    {
        return new Company(corporateName, fantasyName, address, cnpj);
    }


    public void Update(string? fantasyName, Address? address)
    {
        if(address != null) UpdateAddress(address);
        if(fantasyName != null) UpdateFantasyName(fantasyName);
    }

    private void UpdateAddress(Address address)
    {
        var validatedAddres = Address.Create(address.Street, address.Number, address.Complement, address.Neighborhood, address.City, address.State, address.ZipCode);
        Address = validatedAddres;
    }

    private void UpdateFantasyName(string fantasyName)
    {
        FantasyName = fantasyName;
    }

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }

    public void RemovePhoto(Photo photo)
    {
        Photos.Remove(photo);
    }

    public void AddPhoto(Photo photo)
    {
        Photos.Add(photo);
    }

}