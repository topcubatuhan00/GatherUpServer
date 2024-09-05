using GatherUp.Domain.Entities;
using GatherUp.Domain.Repositories.CommunityRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.CommunityRepositories;

public class CommunityCommandRepository : Repository, ICommunityCommandRepository
{
    #region Ctor
    public CommunityCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public async Task AddAsync(Community entity)
    {
        var query = "INSERT INTO [Community]" +
                    "(Name,Description,CreatedDate,CreatorName,DeletedDate,DeleterName,UpdatedDate,UpdaterName, IsActive) VALUES" +
                    "(@name, @desc, @createddate,@creatorname,@deletedDate,@deletername,@updatedate,@updatername, @active);" +
                    "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@desc", entity.Description);
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
        var command = CreateCommand("update [Community] set IsActive=@active,DeletedDate=@ddate, DeleterName=@dname where Id=@id");
        command.Parameters.AddWithValue("@active", false);
        command.Parameters.AddWithValue("@ddate", DateTime.Now);
        command.Parameters.AddWithValue("@dname", "SERVER");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Community entity)
    {
        var query = "update [Community] set Name=@name, Description=@desc, UpdatedDate=@udate, UpdaterName=@uname, IsActive=@active where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@desc", entity.Description);
        command.Parameters.AddWithValue("@udate", entity.UpdatedDate);
        command.Parameters.AddWithValue("@uname", entity.UpdaterName);
        command.Parameters.AddWithValue("@active", entity.IsActive);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }
}
