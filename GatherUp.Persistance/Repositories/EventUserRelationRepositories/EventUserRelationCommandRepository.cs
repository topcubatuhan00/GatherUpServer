using GatherUp.Domain.Entities;
using GatherUp.Domain.Repositories.EventUserRelationRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.EventUserRelationRepositories;

public class EventUserRelationCommandRepository : Repository, IEventUserRelationCommandRepository
{
    #region Ctor
    public EventUserRelationCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public async Task AddAsync(EventUserRelation entity)
    {
        var query = "INSERT INTO [EventUserRelation]" +
            "(EventId, UserId) VALUES" +
            "(@eid, @uid);";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@eid", entity.EventId);
        command.Parameters.AddWithValue("@uid", entity.UserId);
        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("DELETE FROM [EventUserRelation] where Id=@id");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(EventUserRelation entity)
    {
        var query = "update [EventUserRelation] set UserId=@uid, EventId=@eid where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@eid", entity.EventId);
        command.Parameters.AddWithValue("@uid", entity.UserId);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }
}
