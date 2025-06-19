namespace SimDynoServer.Helpers;

public static class SignalRHelper
{
    private static HashSet<string> _requestedFields = new HashSet<string>();
    public static HashSet<string> RequestedFields => _requestedFields;
    
    public static void SetRequestedFields(string[] fields)
    {
        _requestedFields.Clear();

        foreach(var field in fields)
            _requestedFields.Add(field);
    }

    public static bool HasRequestedFields() => _requestedFields.Count > 0;
    public static IEnumerable<string> GetRequestedFields() => _requestedFields;
}
