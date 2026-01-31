using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataMusic.External.Persistence;

public static class MongoExtension
{
    public static IServiceCollection AddMongo(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<MongoDbSettings>()
            .Bind(configuration.GetSection("MongoDbSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

            return new MongoClient(settings.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();

            return client.GetDatabase(settings.DataBaseName);
        });

        return services;
    }
}