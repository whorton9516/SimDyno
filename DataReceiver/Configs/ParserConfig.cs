using SimDynoServer.Utils;

namespace SimDynoServer.Configs;

public class ParserConfig
{
    private readonly object _lock = new();
    private List<Action<byte[], Models.ForzaData>> _enabledParsers = new();
    public IReadOnlyList<Action<byte[], Models.ForzaData>> EnabledParsers
    {
        get { lock (_lock) { return _enabledParsers.ToList(); } }
    }

    public ParserConfig()
    {
        EnableAllParsers();
    }

    // For feedback
    public List<string> LastAcceptedFields { get; private set; } = new();
    public List<string> LastRejectedFields { get; private set; } = new();

    public void EnableParsers(IEnumerable<string> keys)
    {
        lock (_lock)
        {
            _enabledParsers.Clear();
            LastAcceptedFields.Clear();
            LastRejectedFields.Clear();
            foreach (var key in keys)
            {
                if (ParserRegistry.AllParsers.TryGetValue(key, out var action))
                {
                    _enabledParsers.Add(action);
                    LastAcceptedFields.Add(key);
                }
                else
                {
                    LastRejectedFields.Add(key);
                }
            }
        }
    }

    public void EnableAllParsers()
    {
        lock (_lock)
        {
            _enabledParsers = ParserRegistry.AllParsers.Values.ToList();
            LastAcceptedFields = ParserRegistry.AllParsers.Keys.ToList();
            LastRejectedFields.Clear();
        }
    }
}
