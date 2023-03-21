using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;

namespace BF2TV.Frontend.Services.Alerts;

public class AlertSettingsService : IAlertSettingsService
{
    private readonly IJsonRepository<AlertSettings> _alertSettingsRepository;

    public AlertSettingsService(IJsonRepository<AlertSettings> alertSettingsRepository)
    {
        _alertSettingsRepository = alertSettingsRepository;
    }

    public async Task<bool> GetAreAllAlertsEnabled()
    {
        var list = await _alertSettingsRepository.GetAll();
        return list.FirstOrDefault()?.IsEnabled ?? true;
    }

    public async Task SetAreAllAlertsEnabled(bool value)
    {
        // This is suuper quick and dirty.
        // Should introduce generic instance based repo, instead of generic list based repo.

        var oldValue = new AlertSettings {IsEnabled = !value};
        await _alertSettingsRepository.Remove(oldValue);

        var newValue = new AlertSettings {IsEnabled = value};
        await _alertSettingsRepository.Add(newValue);
    }
}