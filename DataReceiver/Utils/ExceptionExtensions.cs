using System.Text;

namespace SimDynoServer.Utils;

public static class ExceptionExtensions
{
    public static void LogException(this Exception ex, string note = "N/A")
    {
        var sb = new StringBuilder();

        sb.AppendLine($"===================== Exception ====================");
        sb.AppendLine($"Time        : {DateTime.UtcNow.ToString("hh:mm:ss")}");
        sb.AppendLine($"Note        : {note}");
        sb.AppendLine($"Type        : {ex.GetType().FullName}");
        sb.AppendLine($"Message     : {ex.Message}");
        sb.AppendLine($"Source      : {ex.Source}");
        sb.AppendLine($"Target      : {ex.TargetSite}");
        sb.AppendLine($"Stack Trace : {ex.StackTrace}");
        sb.AppendLine($"====================================================");

        Console.WriteLine(sb.ToString());
    }
}
