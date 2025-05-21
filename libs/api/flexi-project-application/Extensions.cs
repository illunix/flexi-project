namespace FlexiProject.Application;

public static class Extensions
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddMediator(q => q.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddMappers();
        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
        => services
            .AddSingleton<UserMapper>();
}