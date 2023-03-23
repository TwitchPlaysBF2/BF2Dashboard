using BF2TV.Domain.Models.Alerts;

namespace BF2TV.Domain.Services;

public interface INotificationService
{
    Task NotifyAsync(IConditionStatus status);
    Task RequestPermissions();
}