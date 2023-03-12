namespace BF2TV.Domain.Models.Alerts;

public record FriendCameOnlineAlert(string PlayerName, string ServerName) : IAlert
{
    public string Message => $"{PlayerName} joined Server: {ServerName}";
}