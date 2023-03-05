namespace BF2TV.Domain.Services;

public interface IPeriodicRefresher
{
    bool IsEnabled { get; }
    void UpdateSetting(bool value);
}