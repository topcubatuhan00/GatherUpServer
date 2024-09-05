namespace GatherUp.Domain.UnitOfWork;

public interface IUnitOfWork
{
    IUnitOfWorkAdapter Create();
}
