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
    private bool _isRunning;

    public PeriodicRefresher(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        _timer = new Timer();
        _timer.Interval = TimeSpan.FromSeconds(60).TotalMilliseconds;
        _timer.Elapsed += OnTick;
        _timer.AutoReset = true;
    }

    public void StartRefreshing()
    {
        if (_isRunning)
            return;

        _timer.Start();
        _isRunning = true;
    }

    private void OnTick(object? sender, ElapsedEventArgs e)
    {
        _dispatcher.Dispatch(new InitializeServerListsAction());
    }
}