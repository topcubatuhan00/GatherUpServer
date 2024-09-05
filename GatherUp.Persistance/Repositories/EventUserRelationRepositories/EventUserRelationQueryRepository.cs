using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Repositories.EventUserRelationRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.EventUserRelationRepositories;

public class EventUserRelationQueryRepository : Repository, IEventUserRelationQueryRepository
{
    #region Ctor
    public EventUserRelationQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public PaginationHelper<EventUserRelation> GetAll(PaginationRequest request)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM [EventUserRelation]");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM [EventUserRelation] ORDER BY Id OFFSET {((request.PageNumber - 1) * request.PageSize)} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<EventUserRelation> models = new List<EventUserRelation>();
            while (reader.Read())
            {
                models.Add(new EventUserRelation
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    EventId = Convert.ToInt32(reader["EventId"]),
                });
            }
            return new PaginationHelper<EventUserRelation>(totalCount, request.PageSize, request.PageNumber, models);
        }
    }

    public async Task<EventUserRelation> GetById(int id)
    {
        var command = CreateCommand("SELECT * FROM [EventUserRelation] WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        using (var reader = command.ExecuteReader())
        {
            if (reader.HasRows && reader.Read())
            {
                return new EventUserRelation
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    EventId = Convert.ToInt32(reader["EventId"]),
                };
            }
            else
                return null;
        }
    }
}
