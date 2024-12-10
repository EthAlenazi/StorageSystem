using Microsoft.Extensions.Options;
using SimpleStorageService.Factory;
using SimpleStorageService.Helpers;
using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Implementation;

namespace SimpleStorageService.DependencyInjection
{
    public static class ConfigurationFile
    {
        public static IServiceCollection ReadConfigurationFiles(this IServiceCollection services, IConfiguration configuration)
        {


            // Add specific storage services to DI
            services.AddScoped<S3Storage>();
            services.AddScoped<DatabaseStorage>();
            services.AddScoped<LocalFileSystemStorage>();
            services.AddScoped<FtpStorage>();

            // Register the StorageFactory
            services.AddSingleton<StorageProviderFactory>();

            // Register StorageHandler with dynamic storages
            services.AddScoped<StorageServiceHandler>(serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<StorageProviderFactory>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                // Fetch storage types from configuration or use a default list
                var storageTypes = configuration.GetSection("StorageSettings:EnabledTypes").Get<string[]>()
                                  ?? new[] { "LocalFileSystem" };//"AmazonS3", "Database", 

                // Create storages dynamically
                var storages = factory.CreateStorages(storageTypes);
                return new StorageServiceHandler(storages);
            });


            // Bind settings from configuration
            services.Configure<AmazonS3Settings>(
                configuration.GetSection("StorageSettings:AmazonS3"));
            services.Configure<DatabaseSettings>(
                configuration.GetSection("StorageSettings:Database"));
            services.Configure<FtpSettings>(
                configuration.GetSection("StorageSettings:FTP"));

            // Register strongly-typed settings for direct injection
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<AmazonS3Settings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<LocalFileSystemSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FtpSettings>>().Value);
            return services;

        }
    }
}
