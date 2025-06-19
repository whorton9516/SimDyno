using SimDynoServer.Utils;

namespace SimDynoDevSuite.Services;
public class RecordingService
{
    string _baseDir = AppContext.BaseDirectory;
    string _fileName = string.Empty;
    FileStream? _fileStream;
    BufferedStream? _bufferedStream;

    public void SetFileName(string fileName)
    {
        _fileName = fileName;
    }

    public void StartRecording()
    {
        try
        {
            var folderPath = Path.Combine(_baseDir, @"..\Recordings");
            folderPath = Path.GetFullPath(folderPath);
            Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, _fileName);

            // Open the file for writing (overwrite if exists)
            _fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough);
            _bufferedStream = new BufferedStream(_fileStream, 64 * 1024); // 64KB buffer
        }
        catch (Exception ex)
        {
            ex.LogException("Failed to start recording");
            throw;
        }
    }

    public void Record(byte[] packet, bool isFirstPacket = false)
    {
        try
        {
            if (_bufferedStream == null)
                throw new InvalidOperationException("Recording has not been started.");

            // Record the current timestamp in ticks (long, 8 bytes)
            long timestamp = DateTime.UtcNow.Ticks;
            var timestampBytes = BitConverter.GetBytes(timestamp);
            _bufferedStream.Write(timestampBytes, 0, timestampBytes.Length);

            // Write the packet length (int, 4 bytes)
            var lengthBytes = BitConverter.GetBytes(packet.Length);
            _bufferedStream.Write(lengthBytes, 0, lengthBytes.Length);

            // Write the packet data
            _bufferedStream.Write(packet, 0, packet.Length);
        }
        catch (Exception ex)
        {
            ex.LogException("Failed to record packet");
            throw;
        }
    }

    public void StopRecording()
    {
        try
        {
            if (_bufferedStream != null)
            {
                _bufferedStream.Flush();
                _bufferedStream.Close();
                _bufferedStream = null;
            }
            if (_fileStream != null)
            {
                _fileStream.Close();
                _fileStream = null;
                Console.WriteLine("Recording Saved");
            }
            else
            {
                Console.WriteLine("No recording in progress.");
            }
        }
        catch (Exception ex)
        {
            ex.LogException("Failed to stop recording");
        }
    }

    public string Filename
    {
        get => _fileName;
        set => _fileName = value;
    }
}
