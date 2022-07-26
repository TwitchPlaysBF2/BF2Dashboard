namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2;

/// <summary>
/// Represents a line inside of a .con configuration file.
/// </summary>
public class ConfigSetting
{
    private readonly string _configLine;

    /// <param name="configLine">i.e. LocalProfile.setNick "individuell"</param>
    public ConfigSetting(string configLine)
    {
        _configLine = configLine;
    }

    /// <summary>
    /// Removes the key & other fuzz from a config line and returns only the value.
    /// </summary>
    /// <param name="settingKey">i.e. LocalProfile.setNick</param>
    /// <returns></returns>
    public string ParseSettingValue(string settingKey)
    {
        var value = _configLine
            .Replace(settingKey, "")
            // Note: This type of parsing swallows apostrophes from the setting. RegEx could fix this.
            .Replace("\"", "")
            .Trim();

        return value;
    }
}