using System.Text.RegularExpressions;

namespace BF2TV.Domain.Extensions;

public static class StringExtensions
{
    public static bool TryGetDiscordUrl(this string text, out string discordUrl)
    {
        var regex = new Regex(@"(https?:\/\/)?(www\.)?(discord\.(gg|io|me|li)|discordapp\.com\/invite)\/.+[a-z0-9]", RegexOptions.IgnoreCase);
        var match = regex.Match(text);
        if (match.Success)
        {
            discordUrl = match.Value;
            return true;
        }

        discordUrl = string.Empty;
        return false;
    }
}