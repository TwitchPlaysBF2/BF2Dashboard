namespace BF2TV.Domain.Models;

public record BasicPlayerModel(string PlayerName, string? Prefix)
{
    public override string ToString() => Prefix == null ? PlayerName : $"{Prefix} {PlayerName}";
}