using BF2TV.Domain.Models.Alerts;
using Blazored.LocalStorage;

namespace BF2TV.Domain.Repositories;

public class AlertRepository : IAlertRepository
{
    private static readonly SemaphoreSlim Semaphore = new(1);

    private readonly ILocalStorageService _localStorageService;

    public AlertRepository(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task Add(ConditionId conditionId)
    {
        await Semaphore.WaitAsync();
        try
        {
            var conditionList = await _localStorageService.GetItemAsync<List<string>>(Commons.ConditionListKey)
                                ?? new List<string>();

            conditionList.Add(conditionId.ToString());
            await _localStorageService.SetItemAsync(Commons.ConditionListKey, conditionList);
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task Remove(ConditionId conditionId)
    {
        await Semaphore.WaitAsync();
        try
        {
            var conditionList = await _localStorageService.GetItemAsync<List<string>>(Commons.ConditionListKey)
                                ?? new List<string>();

            conditionList.Remove(conditionId.ToString());
            await _localStorageService.SetItemAsync(Commons.ConditionListKey, conditionList);
        }
        finally
        {
            Semaphore.Release();
        }
    }
}