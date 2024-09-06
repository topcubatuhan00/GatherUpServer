using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Repositories.UserRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.UserRepositories;

public class UserQueryRepository : Repository, IUserQueryRepository
{
    #region Ctor
    public UserQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public PaginationHelper<User> GetAll(PaginationRequest request)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM [User]");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM [User] WHERE IsActive=1 ORDER BY Id OFFSET {((request.PageNumber - 1) * request.PageSize)} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<User> models = new List<User>();
            while (reader.Read())
            {
                models.Add(new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    Picture = reader["Picture"].ToString(),
                    Role = reader["Role"].ToString(),
                    UserName = reader["UserName"].ToString(),
                    IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),

                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    UpdaterName = reader["UpdaterName"] != DBNull.Value ? reader["UpdaterName"].ToString() : null,
                    UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"]) : (DateTime?)null,
                });
            }
            return new PaginationHelper<User>(totalCount, request.PageSize, request.PageNumber, models);
        }
    }

    public async Task<User> GetById(int id)
    {
        var command = CreateCommand("SELECT * FROM [User] WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        using (var reader = command.ExecuteReader())
        {
            if (reader.HasRows && reader.Read())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    Picture = reader["Picture"].ToString(),
                    Role = reader["Role"].ToString(),
                    UserName = reader["UserName"].ToString(),
                    IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),

                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    UpdaterName = reader["UpdaterName"] != DBNull.Value ? reader["UpdaterName"].ToString() : null,
                    UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"]) : (DateTime?)null,
                };
            }
            else
                return null;
        }
    }
}
