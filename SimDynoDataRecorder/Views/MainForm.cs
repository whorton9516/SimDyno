using SimDynoDataRecorder.Services;
using SimDynoDataRecorder.Models;
using SimDynoDataRecorder.Views;
using SimDynoServer.Models;
using System.Net;
using System.ComponentModel;

namespace SimDynoDataRecorder;

public partial class MainForm : Form
{
    readonly ReceiverService _receiverService;
    readonly BroadcastService _broadcastService;
    AppState _state = AppState.Idle;
    bool _listening = false;
    TelemetryDataView _telemetryDataView;
    string _fileName = string.Empty;
    int _packetsReceived = 0;

    readonly HttpClient _httpClient = new HttpClient();
    public event EventHandler<AppState>? StateChanged;

    public MainForm(BroadcastService broadcastService, ReceiverService receiverService)
    {
        InitializeComponent();

        _broadcastService = broadcastService;
        _receiverService = receiverService;

        StateChanged += StateChangedHandler;

        BroadcastButton.Enabled = false;
        _receiverService.UpdateState(_state);
        Directory.CreateDirectory(@"..\Recordings");
        var files = Directory.GetFiles(@"..\Recordings").ToList();
        comboBoxRecordings.DataSource = files;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AppState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                StateChanged?.Invoke(this, _state);
            }
        }
    }

    private void StateChangedHandler(object? sender, AppState newState)
    {
        try
        {
            Console.WriteLine($"State changed to: {newState}");

            switch (_state)
            {
                case AppState.Idle:
                    ListenButton.Text = "Start Listening";
                    RecordButton.Text = "Record";
                    BroadcastButton.Text = "Broadcast";
                    ListenButton.Enabled = true;
                    RecordButton.Enabled = true;
                    if (comboBoxRecordings.SelectedItem != null)
                        BroadcastButton.Enabled = true;
                    _listening = false;
                    break;
                case AppState.Listen:
                    ListenButton.Text = "Stop Listening";
                    RecordButton.Text = "Record";
                    RecordButton.Enabled = true;
                    BroadcastButton.Text = "Broadcast";
                    BroadcastButton.Enabled = false;
                    _listening = true;
                    break;
                case AppState.Recording:
                    ListenButton.Text = "Stop Listening";
                    ListenButton.Enabled = true;
                    RecordButton.Text = "Stop Recording";
                    BroadcastButton.Text = "Broadcast";
                    BroadcastButton.Enabled = false;
                    _listening = true;
                    break;
                case AppState.Broadcast:
                    ListenButton.Text = "Start Listening";
                    ListenButton.Enabled = false;
                    RecordButton.Text = "Record";
                    RecordButton.Enabled = false;
                    BroadcastButton.Text = "Stop Broadcasting";
                    _listening = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when _updating the AppState: {ex.Message}");
        }
    }

    private async void ListenButton_Click(object sender, EventArgs e)
    {
        switch (_state)
        {
            case AppState.Idle:
                StartListening();
                break;
            case AppState.Listen:
                StopListening();
                break;
            case AppState.Recording:
                StopRecording();
                StopListening();
                break;
        }
    }

    void StartListening()
    {
        try
        {
            if (_state == AppState.Idle)
                UpdateState(AppState.Listen);
            _packetsReceived = 0;
            _receiverService.UpdateState(AppState.Listen);
            _receiverService.StartListening(this, IPAddressTextBox.Text, PortTextBox.Text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran into an issue when starting the Listener: {ex.Message}");
            StopListening();
        }
    }

    void StopListening()
    {
        try
        {
            UpdateState(AppState.Idle);
            _listening = false;
            _receiverService.UpdateState(_state);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran into an issue when stoping the Listener: {ex.Message}");
            Close();
        }
    }

    async Task StartBroadcasting()
    {
        try
        {
            // Automatically start the Listener on the Server
            await StartRemoteListenerAsync();

            UpdateState(AppState.Broadcast);
            _receiverService.UpdateState(_state);
            _receiverService.Listener?.Dispose();

            var packets = GetPackets(_fileName);
            Task.Run(() => _broadcastService.StartBroadcasting(packets, _telemetryDataView));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran into an issue when starting the Broadcast: {ex.Message}");
            StopBroadcasting();
        }
    }

    async Task StopBroadcasting()
    {
        try
        {
            // Automatically stop the Listener on the Server
            await StopRemoteListenerAsync();

            UpdateState(AppState.Idle);
            _receiverService.UpdateState(_state);
            _broadcastService.StopBroadcasting();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran Into an issue when stopping the broadcast: {ex.Message}");
            Close();
        }
    }

    void StartRecording()
    {
        try
        {
            UpdateState(AppState.Recording);
            _receiverService.UpdateState(_state);
            //_receiverService.StartListening(this, IPAddressTextBox.Text, PortTextBox.Text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran Into an issue when starting the Recording: {ex.Message}");
        }
    }

    void StopRecording()
    {
        try
        {
            if (_listening)
                UpdateState(AppState.Listen);
            else
                UpdateState(AppState.Idle);

            _receiverService.UpdateState(_state);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ran into an issue when stopping the Recording: {ex.Message}");
            Close();
        }
    }

    private async void BroadcastButton_Click(object sender, EventArgs e)
    {
        switch (_state)
        {
            case AppState.Idle:
            case AppState.Listen:
                StopListening();
                StartBroadcasting();
                break;
            case AppState.Broadcast:
                StopBroadcasting();
                break;
        }
        var ip = IPAddressTextBox.Text;
        var port = PortTextBox.Text;

        _receiverService.UpdateState(AppState.Broadcast);
    }

    private async void RecordButton_Click(object sender, EventArgs e)
    {
        switch (_state)
        {
            case AppState.Idle:
            case AppState.Listen:
                StartListening();
                StartRecording();
                break;
            case AppState.Recording:
                StopRecording();
                break;
        }
    }

    private void ViewDataButton_Click(object sender, EventArgs e)
    {
        if (_telemetryDataView != null)
        {
            _telemetryDataView.Close();
            _telemetryDataView.Dispose();
            _telemetryDataView = null;
        }

        _telemetryDataView = new TelemetryDataView();
        _telemetryDataView.Show();
        _telemetryDataView.UpdateData(new ForzaData());
    }

    public void UpdateTelemetryDataView(ForzaData data)
    {
        if (_telemetryDataView != null && !_telemetryDataView.Updating)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateTelemetryDataView(data)));
                return;
            }

            try
            {
                if (_telemetryDataView != null)
                {
                    _telemetryDataView.UpdateData(data);
                    _packetsReceived++;
                    labelPacketsReceived.Text = _packetsReceived.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ran into an issue when _updating the TelemetryDataView: {ex.Message}");
            } 
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopRecording();
        Console.WriteLine("Closing the Data Recorder.");
    }

    private void comboBoxRecordings_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxRecordings.SelectedItem != null)
        {
            _fileName = comboBoxRecordings.SelectedItem.ToString() ?? string.Empty;
            BroadcastButton.Enabled = true;
        }
        else
        {
            _fileName = string.Empty;
        }
    }

    private List<byte[]> GetPackets(string filename)
    {
        var packets = new List<byte[]>();
        var filePath = Path.Combine(@"..\Recordings", filename);

        try
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    // Read the length prefix (4 bytes)
                    byte[] lengthBytes = new byte[4];
                    int read = fs.Read(lengthBytes, 0, 4);
                    if (read < 4) break;

                    int packetLength = BitConverter.ToInt32(lengthBytes, 0);

                    // Read the packet
                    byte[] packet = new byte[packetLength];
                    read = fs.Read(packet, 0, packetLength);
                    if (read < packetLength) break;

                    packets.Add(packet);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error when accessing file: {filename}\n{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return packets;
    }


    private void IPAddressTextBox_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(IPAddressTextBox.Text))
        {
            if (!IPAddress.TryParse(IPAddressTextBox.Text, out _))
            {
                IPAddressTextBox.BackColor = Color.Red;
                IPAddressTextBox.Text = "127.0.0.1";
            }
            else
            {
                IPAddressTextBox.BackColor = Color.White;
            }
        }
    }

    private void PortTextBox_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(PortTextBox.Text))
        {
            if (!int.TryParse(PortTextBox.Text, out _) || PortTextBox.Text.Length >= 5)
            {
                PortTextBox.BackColor = Color.Red;
                PortTextBox.Text = "5555";
            }
            else
            {
                PortTextBox.BackColor = Color.White;
            }
        }
    }

    private void UpdateState(AppState state)
    {
        State = state;
    }

    bool IsValidBase64(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return false;

        if (str.Length % 4 != 0)
            return false;

        return str.All(c => char.IsLetterOrDigit(c) || c == '+' || c == '/' || c == '=');
    }

    string SanitizeString(string str)
    {
        var sanitizedString = str.Replace("\"", "").Replace(",", "").Replace("[", "").Replace("]", "").Trim();
        return sanitizedString;
    }

    private async Task StartRemoteListenerAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("http://localhost:5000/api/simdyno/listener/start", null);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to start remote listener.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting remote listener: {ex.Message}");
        }
    }

    private async Task StopRemoteListenerAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("http://localhost:5000/api/simdyno/listener/stop", null);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to stop remote listener.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error stopping remote listener: {ex.Message}");
        }
    }
}