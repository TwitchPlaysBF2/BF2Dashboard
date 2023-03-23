namespace BF2TV.Frontend.Services.Alerts;

public interface IAlertSettingsService
{
    Task<bool> GetAreAllAlertsEnabled();
    Task SetAreAllAlertsEnabled(bool value);
}