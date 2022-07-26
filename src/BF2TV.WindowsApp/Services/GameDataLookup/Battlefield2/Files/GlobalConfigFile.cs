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
}