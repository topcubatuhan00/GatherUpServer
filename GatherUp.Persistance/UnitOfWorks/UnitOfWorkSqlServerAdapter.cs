using GatherUp.Domain.UnitOfWork;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.UnitOfWorks;

public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
{
    #region Fields
    private SqlConnection _context { get; set; }
    private SqlTransaction _transaction { get; set; }
    public IUnitOfWorkRepository Repositories { get; set; }
    #endregion

    #region Ctor
    public UnitOfWorkSqlServerAdapter
    (
        string connectionString
    )
    {
        _context = new SqlConnection(connectionString);
        _context.Open();
        _transaction = _context.BeginTransaction();
        Repositories = new UnitOfWorkSqlServerRepository(_context, _transaction);
    }
    #endregion
    #region Methods
    public void Dispose()
    {
        if (_transaction != null)
            _transaction.Dispose();
        if (_context != null)
        {
            _context.Close();
            _context.Dispose();
        }

        Repositories = null;
    }

    public void SaveChanges()
    {
        _transaction.Commit();
    }
    #endregion
}
