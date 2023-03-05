using System.Timers;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Store;
using Fluxor;
using Timer = System.Timers.Timer;

namespace BF2TV.Frontend.Services;

public class PeriodicRefresher : IPeriodicRefresher
{
    private readonly IDispatcher _dispatcher;
    private readonly Timer _timer;

    public PeriodicRefresher(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        _timer = new Timer();
        _timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
        _timer.Elapsed += OnTick;
        _timer.AutoReset = true;
    }

    public bool IsEnabled
    {
        get => _timer.Enabled;
        private set => _timer.Enabled = value;
    }

    public void UpdateSetting(bool shouldEnable)
    {
        if (shouldEnable && IsEnabled)
            return;

        IsEnabled = shouldEnable;

        if (IsEnabled)
        {
            // Refresh now, when toggling on
            _dispatcher.Dispatch(new InitializeServerListsAction());
        }
    }

    private void OnTick(object? sender, ElapsedEventArgs e)
    {
        _dispatcher.Dispatch(new InitializeServerListsAction());
    }
}