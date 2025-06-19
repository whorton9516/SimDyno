using SimDynoServer.Hubs;
using SimDynoServer.Models;
using SimDynoServer.Services;
using SimDynoServer.Utils;

namespace SimDynoServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
            });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("https://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        builder.Services.AddSingleton<ReceiverService>();
        builder.Services.AddHostedService<ReceiverHostedService>();
        builder.Services.AddSingleton<LoggingUtil>();

        builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseCors();

        // Replace UseEndpoints with top-level route registrations
        app.MapControllers();
        app.MapHub<SimDynoHub>("/simDynoHub");

        var loggingUtil = app.Services.GetRequiredService<LoggingUtil>();
        var urls = builder.WebHost.GetSetting("urls");

        loggingUtil.SetSignalRStatus(SignalRStatus.WaitingOnClients, false);
        loggingUtil.SetSignalRUrl(urls, false);

        app.Run();
    }
}
