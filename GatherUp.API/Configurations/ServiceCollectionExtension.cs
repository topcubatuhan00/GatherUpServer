using GatherUp.Application.Services;
using GatherUp.Domain.UnitOfWork;
using GatherUp.Persistance.Mapping;
using GatherUp.Persistance.Services;
using GatherUp.Persistance.UnitOfWorks;

namespace GatherUp.API.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ApplicationServiceConfigurations(this IServiceCollection services)
    {
        #region AppScopes
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventUserRelationService, EventUserRelationService>();
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region Utilities
        services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        #endregion

        return services;
    }
}
