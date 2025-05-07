using System.Text;
using System.Text.Json;

namespace SimDynoDataRecorder.Services;
public class RecordingService
{
    string _baseDir = AppContext.BaseDirectory;
    string _fileName = string.Empty;
    List<string> _packets;

    public RecordingService()
    {
        _packets = new List<string>();
    }

    public void Record(byte[] packet)
    {
        var packetString = Convert.ToBase64String(packet);
        _packets.Add(packetString);
    }

    public void SetFileName(string fileName)
    {
        _fileName = fileName;
    }

    public void StopRecording()
    {
        Console.WriteLine("Recording Stopped.");
        SaveRecording(_packets, _fileName);
    }

    private void SaveRecording(List<string> packets, string filename)
    {
        try
        {
            var folderPath = Path.Combine(_baseDir, @"..\Recordings");
            folderPath = Path.GetFullPath(folderPath);
            var filePath = Path.Combine(folderPath, filename);

            File.WriteAllText(filePath, JsonSerializer.Serialize(packets));
            Console.WriteLine("Recording Saved");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error while saving the recording: {ex.Message}");
        }
    }

    public string Filename
    {
        get => _fileName;
        set => _fileName = value;
    }
}
