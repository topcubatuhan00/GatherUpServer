using GatherUp.Domain.Entities;
using GatherUp.Domain.Repositories.UserRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.Repositories.UserRepositories;

public class UserCommandRepository : Repository, IUserCommandRepository
{
    #region Ctor
    public UserCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    #endregion
    public async Task RemoveById(int id)
    {
        var command = CreateCommand("update [User] set IsActive=@active,DeletedDate=@ddate, DeleterName=@dname where Id=@id");
        command.Parameters.AddWithValue("@active", false);
        command.Parameters.AddWithValue("@ddate", DateTime.Now);
        command.Parameters.AddWithValue("@dname", "SERVER");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        var query = "update [User] set Name=@name, LastName=@lname, UserName=@uname, Email=@email, Password=@pwd, Role=@role, IsConfirmed=@isConf, Picture=@picture, UpdatedDate=@udate, UpdaterName=@udname, IsActive=@active where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@lname", entity.LastName);
        command.Parameters.AddWithValue("@uname", entity.UserName);
        command.Parameters.AddWithValue("@email", entity.Email);
        command.Parameters.AddWithValue("@pwd", entity.Password);
        command.Parameters.AddWithValue("@role", entity.Role);
        command.Parameters.AddWithValue("@isConf", entity.IsConfirmed);
        command.Parameters.AddWithValue("@picture", entity.Picture);

        command.Parameters.AddWithValue("@udate", DateTime.Now);
        command.Parameters.AddWithValue("@udname", entity.UpdaterName);
        command.Parameters.AddWithValue("@active", entity.IsActive);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }
}
