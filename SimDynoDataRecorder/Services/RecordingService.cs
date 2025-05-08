namespace SimDynoDataRecorder.Services;
public class RecordingService
{
    string _baseDir = AppContext.BaseDirectory;
    string _fileName = string.Empty;
    FileStream? _fileStream;

    public void SetFileName(string fileName)
    {
        _fileName = fileName;
    }

    public void StartRecording()
    {
        var folderPath = Path.Combine(_baseDir, @"..\Recordings");
        folderPath = Path.GetFullPath(folderPath);
        Directory.CreateDirectory(folderPath); // Ensure directory exists
        var filePath = Path.Combine(folderPath, _fileName);

        // Open the file for writing (overwrite if exists)
        _fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    public void Record(byte[] packet, bool isFirstPacket = false)
    {
        if (_fileStream == null)
            throw new InvalidOperationException("Recording has not been started.");

        // Write the length of the packet (as 4 bytes, little-endian)
        var lengthBytes = BitConverter.GetBytes(packet.Length);
        _fileStream.Write(lengthBytes, 0, lengthBytes.Length);

        // Write the actual packet bytes
        _fileStream.Write(packet, 0, packet.Length);
    }

    public void StopRecording()
    {
        if (_fileStream != null)
        {
            _fileStream.Flush();
            _fileStream.Close();
            _fileStream = null;
            Console.WriteLine("Recording Saved");
        }
        else
        {
            Console.WriteLine("No recording in progress.");
        }
    }

    public string Filename
    {
        get => _fileName;
        set => _fileName = value;
    }
}
