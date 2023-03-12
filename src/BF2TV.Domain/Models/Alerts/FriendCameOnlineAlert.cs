namespace BF2TV.Domain.Models.Alerts;

public record FriendCameOnlineAlert(string PlayerName, ServerInfoModel ServerInfo, DateTime WhenUtc) : IAlert;