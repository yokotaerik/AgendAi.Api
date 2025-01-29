namespace MarcAI.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
    Task Roolback();
}
