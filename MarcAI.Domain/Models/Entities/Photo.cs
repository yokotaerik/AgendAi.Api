using MarcAI.Domain.Enums;
using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class Photo : BaseEntity
{
    public string Url { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public EntitiesAssociation EntityType { get; private set; } // "Company" ou "Employee"

    private Photo() { }


    private Photo(string url, Guid entityId, EntitiesAssociation entityType) : base()
    {
        Url = url;
        EntityType = entityType;

        switch (entityType)
        {
            case EntitiesAssociation.Company:
                CompanyId = entityId;
                break;
            case EntitiesAssociation.Employee:
                EmployeeId = entityId;
                break;
            default:
                throw new ArgumentException("Tipo de entidade inválido", nameof(entityType));
        }
    }

    public static Photo Create(string url, Guid entityId, EntitiesAssociation entityType)
    {
        return new Photo(url, entityId, entityType);
    }
}
