using SimDynoServer.Hubs;
using SimDynoServer.Services;

namespace DataReceiver;

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

        builder.Services.AddSingleton<Receiver>();
        builder.Services.AddHostedService<ReceiverHostedService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<SimDynoHub>("/simDynoHub");
        });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        
        var serverUrls = builder.WebHost.GetSetting("urls") ?? "http://localhost:5000;https://localhost:5001";
        Console.WriteLine($"Yay! Server started on URL(s): {serverUrls}");

        app.Run();
    }
}
