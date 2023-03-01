using System.Text.RegularExpressions;

namespace BF2TV.Domain.Services;

public class DiscordUrlParser
{
    public bool TryGetDiscordUrl(string textToParse, out string discordUrl)
    {
        var regex = new Regex(@"(?:https?://)?discord(?:(?:app)?\.com/invite|\.gg)/?[a-zA-Z0-9]+/?", RegexOptions.IgnoreCase);
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