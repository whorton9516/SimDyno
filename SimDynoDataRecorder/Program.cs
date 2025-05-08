using Microsoft.Extensions.DependencyInjection;
using SimDynoDataRecorder.Services;
using SimDynoDataRecorder.Views;
using System.Runtime.InteropServices;

namespace SimDynoDataRecorder;

internal static class Program
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AllocConsole();
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        AllocConsole();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var services = new ServiceCollection();

        services.AddSingleton<BroadcastService>(provider =>
            new BroadcastService("127.0.0.1", "5555"));
        services.AddSingleton<ReceiverService>();
        services.AddSingleton<RecordingService>();

        services.AddSingleton<MainForm>();

        var serviceProvider = services.BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        var mainForm = serviceProvider.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }
}