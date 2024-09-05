using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Repositories.EventRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.EventRepositories;

public class EventQueryRepository : Repository, IEventQueryRepository
{
    #region Ctor
    public EventQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }

    public PaginationHelper<Event> GetAll(PaginationRequest request)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM [Event]");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM [Event] ORDER BY Id OFFSET {((request.PageNumber - 1) * request.PageSize)} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<Event> models = new List<Event>();
            while (reader.Read())
            {
                models.Add(new Event
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CommunityId = Convert.ToInt32(reader["CommunityId"]),
                    Name = reader["Name"].ToString(),
                    CommunityName = reader["CommunityName"].ToString(),
                    EventDate = reader["EventDate"].ToString(),
                    EventPlace = reader["EventPlace"].ToString(),
                    EventTime = reader["EventTime"].ToString(),
                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                });
            }
            return new PaginationHelper<Event>(totalCount, request.PageSize, request.PageNumber, models);
        }
    }

    public async Task<Event> GetById(int id)
    {
        var command = CreateCommand("SELECT * FROM [Event] WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        using (var reader = command.ExecuteReader())
        {
            if (reader.HasRows && reader.Read())
            {
                return new Event
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CommunityId = Convert.ToInt32(reader["CommunityId"]),
                    Name = reader["Name"].ToString(),
                    CommunityName = reader["CommunityName"].ToString(),
                    EventDate = reader["EventDate"].ToString(),
                    EventPlace = reader["EventPlace"].ToString(),
                    EventTime = reader["EventTime"].ToString(),
                    CreatorName = reader["CreatorName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                };
            }
            else
                return null;
        }
    }
    #endregion
}
