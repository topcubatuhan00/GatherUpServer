namespace GatherUp.API.Configurations;

public static class JwtServiceCollectionExtension
{
    public static IServiceCollection JwtServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}
