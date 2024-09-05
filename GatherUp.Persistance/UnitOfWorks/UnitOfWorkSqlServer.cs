using GatherUp.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace GatherUp.Persistance.UnitOfWorks;

public class UnitOfWorkSqlServer : IUnitOfWork
{
    #region Fields
    private readonly IConfiguration _configuration;
    #endregion

    #region Ctor
    public UnitOfWorkSqlServer
    (
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }
    #endregion

    #region Methods
    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("GatherUpConnection");
    }
    public IUnitOfWorkAdapter Create()
    {
        var connectionString = GetConnectionString();
        return new UnitOfWorkSqlServerAdapter(connectionString);
    }
    #endregion
}
