using MarcAI.Domain.Enums;
using MarcAI.Domain.Models.Common;
using System;

namespace MarcAI.Domain.Models.Entities;

public class Photo : BaseEntity
{
    public string PathName { get; private set; } = null!;
    public string WebUrl { get; private set; } = null!;
    public Guid? CompanyId { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public EntitiesAssociation EntityType { get; private set; } 

    private Photo() { }


    private Photo(string pathName, Guid entityId, EntitiesAssociation entityType) : base()
    {
        PathName = pathName;
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

    public static Photo Create(string pathName,  Guid entityId, EntitiesAssociation entityType)
    {
        return new Photo(pathName, entityId, entityType);
    }

    public void SetWebUrl(string webUrl)
    {
        WebUrl = webUrl;
    }   
}
