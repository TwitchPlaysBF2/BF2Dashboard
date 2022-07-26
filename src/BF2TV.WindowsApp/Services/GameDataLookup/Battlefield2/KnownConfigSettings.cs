using BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;

namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2;

public static class KnownConfigSettings
{
    public static string GetCurrentlyActiveProfileNumber<T>(this ConfigFile<T> configFile)
        where T : GlobalConfigFile
    {
        var profileNumber = configFile.GetSettingValue("GlobalSettings.setDefaultUser");
        if (!int.TryParse(profileNumber, out _))
            throw new ArgumentException(message:
                $"Couldn't parse profile number from config file: {configFile.FilePath}" +
                $"Illegal format, expected only numbers in profile number string: {profileNumber}");

        // Don't return int otherwise we lose leading zeros
        return profileNumber;
    }

    public static string GetPlayerName<T>(this ConfigFile<T> configFile)
        where T : ProfileConfigFile
    {
        return configFile.GetSettingValue("LocalProfile.setNick");
    }
}