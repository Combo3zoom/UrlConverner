using DataLayer.Repository;
using DataLayer.Repository.Interface;

namespace Inforce;

public static class ServicesExtentions
{
    public static void AddDataLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IUrlsRepository, UrlsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDescriptionRepository, DescriptionRepository>();
    }
}