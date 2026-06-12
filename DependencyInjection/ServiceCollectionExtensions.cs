using MediaStorage.Abstractions.Interfaces;
using MediaStorage.Cloudinary;
using Microsoft.Extensions.DependencyInjection;

namespace MediaStorage.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediaStorage(this IServiceCollection services)
    {
        services.AddScoped<IMediaStorageService, CloudinaryMediaStorageService>();

        return services;
    }

    public static IServiceCollection AddMediaStorageWithCloudinary(this IServiceCollection services, Action<CloudinaryOptions> configure)
    {
        services.Configure(configure);

        services.AddScoped<IMediaStorageService, CloudinaryMediaStorageService>();

        return services;
    }
}
