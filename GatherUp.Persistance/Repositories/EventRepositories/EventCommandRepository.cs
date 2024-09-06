using GatherUp.Domain.Entities;
using GatherUp.Domain.Repositories.EventRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.EventRepositories;

public class EventCommandRepository : Repository, IEventCommandRepository
{
    #region Ctor
    public EventCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public async Task AddAsync(Event entity)
    {
        var query = "INSERT INTO [Event]" +
                    "(Name,EventDate,EventTime,EventPlace,CommunityName,CommunityId,CreatedDate,CreatorName,DeletedDate,DeleterName,UpdatedDate,UpdaterName, IsActive) VALUES" +
                    "(@name, @edate,@etime,@eplace,@cname,@cid, @createddate,@creatorname,@deletedDate,@deletername,@updatedate,@updatername, @active);" +
                    "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@edate", entity.EventDate);
        command.Parameters.AddWithValue("@etime", entity.EventTime);
        command.Parameters.AddWithValue("@eplace", entity.EventPlace);
        command.Parameters.AddWithValue("@cname", entity.CommunityName);
        command.Parameters.AddWithValue("@cid", entity.CommunityId);
        command.Parameters.AddWithValue("@createddate", DateTime.Now);
        command.Parameters.AddWithValue("@creatorname", entity.CreatorName);
        command.Parameters.AddWithValue("@deletedDate", DBNull.Value);
        command.Parameters.AddWithValue("@deletername", DBNull.Value);
        command.Parameters.AddWithValue("@updatedate", DBNull.Value);
        command.Parameters.AddWithValue("@updatername", DBNull.Value);
        command.Parameters.AddWithValue("@active", true);
        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("update [Event] set IsActive=@active,DeletedDate=@ddate, DeleterName=@dname where Id=@id");
        command.Parameters.AddWithValue("@active", false);
        command.Parameters.AddWithValue("@ddate", DateTime.Now);
        command.Parameters.AddWithValue("@dname", "SERVER");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Event entity)
    {
        var query = "update [Event] set Name=@name, EventDate=@edate, EventTime=@etime, EventPlace=@eplace, CommunityName=@cname, CommunityId=@cid, UpdatedDate=@udate, UpdaterName=@uname, IsActive=@active where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@edate", entity.EventDate);
        command.Parameters.AddWithValue("@etime", entity.EventTime);
        command.Parameters.AddWithValue("@eplace", entity.EventPlace);
        command.Parameters.AddWithValue("@cname", entity.CommunityName);
        command.Parameters.AddWithValue("@cid", entity.CommunityId);

        command.Parameters.AddWithValue("@udate", DateTime.Now);
        command.Parameters.AddWithValue("@uname", entity.UpdaterName);
        command.Parameters.AddWithValue("@active", entity.IsActive);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }
}
