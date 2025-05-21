namespace FlexiProject.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
        => services.AddDbContext<IFlexiProjectDbContext, FlexiProjectDbContext>();
}