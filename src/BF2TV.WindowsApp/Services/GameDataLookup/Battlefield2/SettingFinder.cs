namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2;

public class SettingFinder
{
    private readonly IEnumerable<string> _configLines;

    public SettingFinder(IEnumerable<string> configLines)
    {
        _configLines = configLines;
    }

    public ConfigSetting FindSetting(string lineToFind)
    {
        foreach (var configLine in _configLines)
        {
            if (!configLine.Contains(lineToFind))
                continue;

            return new ConfigSetting(configLine);
        }

        throw new InvalidOperationException(message:
            $"Couldn't find config line: {lineToFind}\r\n" +
            "Given config lines were:\r\n" +
            string.Join("\r\n", _configLines));
    }
}