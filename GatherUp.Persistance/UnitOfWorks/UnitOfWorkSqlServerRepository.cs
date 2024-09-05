using GatherUp.Domain.Repositories.AuthRepositories;
using GatherUp.Domain.Repositories.CommunityRepositories;
using GatherUp.Domain.Repositories.EventRepositories;
using GatherUp.Domain.Repositories.EventUserRelationRepositories;
using GatherUp.Domain.UnitOfWork;
using GatherUp.Persistance.Repositories.AuthRepositories;
using GatherUp.Persistance.Repositories.CommunityRepositories;
using GatherUp.Persistance.Repositories.EventRepositories;
using GatherUp.Persistance.Repositories.EventUserRelationRepositories;
using Microsoft.Data.SqlClient;

namespace GatherUp.Persistance.UnitOfWorks;

public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
{
    #region AuthRepositories
    public IAuthCommandRepository authCommandRepository { get; }
    public IAuthQueryRepository authQueryRepository { get; }
    #endregion

    #region EventRepositories
    public IEventCommandRepository eventCommandRepository { get; }
    public IEventQueryRepository eventQueryRepository { get; }
    #endregion

    #region CommunityRepositories
    public ICommunityCommandRepository communityCommandRepository { get; }
    public ICommunityQueryRepository communityQueryRepository { get; }
    #endregion

    #region EventUserRelationRepositories
    public IEventUserRelationCommandRepository eventUserRelationCommandRepository { get; }
    public IEventUserRelationQueryRepository eventUserRelationQueryRepository { get; }
    #endregion

    #region Ctor
    public UnitOfWorkSqlServerRepository
    (
        SqlConnection context,
        SqlTransaction transaction
    )
    {
        authQueryRepository = new AuthQueryRepository(context, transaction);
        authCommandRepository = new AuthCommandRepository(context, transaction);
        eventCommandRepository = new EventCommandRepository(context, transaction);
        eventQueryRepository = new EventQueryRepository(context, transaction);
        communityCommandRepository = new CommunityCommandRepository(context, transaction);
        communityQueryRepository = new CommunityQueryRepository(context, transaction);
        eventUserRelationCommandRepository = new EventUserRelationCommandRepository(context, transaction);
        eventUserRelationQueryRepository = new EventUserRelationQueryRepository(context, transaction);
    }
    #endregion
}
