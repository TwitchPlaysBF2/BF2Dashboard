namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;

public class GlobalConfigFile : ConfigFile<GlobalConfigFile>
{
    public override string FilePath
    {
        get
        {
            var userDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return $@"{userDocuments}\Battlefield 2\Profiles\Global.con";
        }
    }
    
    public string GetCurrentlyActiveProfileNumber()
    {
        var profileNumber = GetSettingValue("GlobalSettings.setDefaultUser");
        if (!int.TryParse(profileNumber, out _))
            throw new ArgumentException(message:
                $"Couldn't parse profile number from config file: {FilePath}" +
                $"Illegal format, expected only numbers in profile number string: {profileNumber}");

        // Don't return int otherwise we lose leading zeros
        return profileNumber;
    }
}