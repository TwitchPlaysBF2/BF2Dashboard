namespace BF2TV.Domain.Models.Alerts;

public record FriendCameOnlineAlert(string PlayerName, string ServerName, DateTime WhenUtc) : IAlert;