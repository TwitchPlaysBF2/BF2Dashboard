using System.Text.RegularExpressions;

namespace BF2TV.Domain.Services;

public class DiscordUrlParser
{
    public bool TryGetDiscordUrl(string textToParse, out string discordUrl)
    {
        var regex = new Regex(@"(https?:\/\/)?(www\.)?(discord\.(gg|io|me|li)|discordapp\.com\/invite)\/.+[a-z0-9]", RegexOptions.IgnoreCase);
        var match = regex.Match(textToParse);
        if (match.Success)
        {
            discordUrl = match.Value;
            return true;
        }

        discordUrl = string.Empty;
        return false;
    }
}