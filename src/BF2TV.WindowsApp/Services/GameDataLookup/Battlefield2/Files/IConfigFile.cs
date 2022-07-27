namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;

public abstract class ConfigFile<T>
{
    public abstract string FilePath { get; }

    public string GetSettingValue(string settingKey)
    {
        var settingFinder = ReadConfigFile();
        var setting = settingFinder.FindSetting(settingKey);
        var value = setting.ParseSettingValue(settingKey);
        return value;
    }

    private SettingFinder ReadConfigFile()
    {
        if (!File.Exists(FilePath))
            throw new InvalidOperationException($"Couldn't find {typeof(T).FullName} at location: {FilePath}");

        var configLines = File.ReadAllLines(FilePath);
        var finder = new SettingFinder(configLines);
        return finder;
    }
}