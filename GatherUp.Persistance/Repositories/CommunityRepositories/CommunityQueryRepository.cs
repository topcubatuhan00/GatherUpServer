using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Repositories.CommunityRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.CommunityRepositories;

public class CommunityQueryRepository : Repository, ICommunityQueryRepository
{
    #region Ctor
    public CommunityQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public PaginationHelper<Community> GetAll(PaginationRequest request)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM [Community]");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM [Community] ORDER BY Id OFFSET {((request.PageNumber - 1) * request.PageSize)} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<Community> models = new List<Community>();
            while (reader.Read())
            {
                models.Add(new Community
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Description = reader["Description"].ToString(),
                });
            }
            return new PaginationHelper<Community>(totalCount, request.PageSize, request.PageNumber, models);
        }
    }

    public async Task<Community> GetById(int id)
    {
        var command = CreateCommand("SELECT * FROM [Community] WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        using (var reader = command.ExecuteReader())
        {
            if (reader.HasRows && reader.Read())
            {
                return new Community
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Description = reader["Description"].ToString(),
                };
            }
            else
                return null;
        }
    }
}
