using GatherUp.Domain.Repositories.AuthRepositories;
using GatherUp.Domain.Repositories.CommunityRepositories;
using GatherUp.Domain.Repositories.EventRepositories;
using GatherUp.Domain.Repositories.EventUserRelationRepositories;

namespace GatherUp.Domain.UnitOfWork;

public interface IUnitOfWorkRepository
{
    #region AuthRepositories
    IAuthCommandRepository authCommandRepository { get; }
    IAuthQueryRepository authQueryRepository { get; }
    #endregion

    #region CommunityRepositories
    ICommunityCommandRepository communityCommandRepository { get; }
    ICommunityQueryRepository communityQueryRepository { get; }
    #endregion

    #region EventRepositories
    IEventCommandRepository eventCommandRepository { get; }
    IEventQueryRepository eventQueryRepository { get; }
    #endregion

    #region EventUserRelationRepositories
    IEventUserRelationCommandRepository eventUserRelationCommandRepository { get; }
    IEventUserRelationQueryRepository eventUserRelationQueryRepository { get; }
    #endregion
}
