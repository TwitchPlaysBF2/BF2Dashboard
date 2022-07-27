namespace BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;

public class ProfileConfigFile : ConfigFile<ProfileConfigFile>
{
    private readonly string _profileNumber;

    public ProfileConfigFile(string profileNumber)
    {
        _profileNumber = profileNumber;
    }

    public override string FilePath
    {
        get
        {
            var userDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return $@"{userDocuments}\Battlefield 2\Profiles\{_profileNumber}\Profile.con";
        }
    }
    
    public string GetPlayerName() => GetSettingValue("LocalProfile.setGamespyNick");
}