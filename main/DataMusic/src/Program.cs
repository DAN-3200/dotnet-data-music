using DataMusic.External.Adapters;
using DataMusic.External.Middlewares;
using DataMusic.External.Persistence;
using DataMusic.External.Persistence.Repository;
using DataMusic.Internal.Ports;
using DataMusic.Internal.Usecases;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddOpenApi();

    builder.Services.AddScoped<MusicUsecase>();
    builder.Services.AddScoped<AlbumUsecase>();
    builder.Services.AddScoped<ArtistUsecase>();

    builder.Services.AddScoped<IMusicRepo, MusicRepo>();
    builder.Services.AddScoped<IAlbumRepo, AlbumRepo>();
    builder.Services.AddScoped<IArtistRepo, ArtistRepo>();
    builder.Services.AddScoped<IServiceHttp, ServiceHttp>();
    builder.Services.AddScoped<HttpClient>();

    builder.Services.AddMongo(builder.Configuration);
    builder.Services.AddControllers();

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
    builder.Host.UseSerilog();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseGlobalErrorHandler();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.MapControllers();
}

app.Run();

