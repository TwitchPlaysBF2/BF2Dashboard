using System.Text.Json;
using Blazored.LocalStorage;

namespace BF2TV.Domain.Repositories;

public class JsonRepository<T> : IJsonRepository<T>
    where T : IEquatable<T>
{
    private readonly SemaphoreSlim _semaphore = new(1);
    private readonly ILocalStorageService _localStorageService;
    private readonly string _storageKey = Commons.ConditionListKey;

    public JsonRepository(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task Add(T instance)
    {
        await _semaphore.WaitAsync();
        try
        {
            var list = await GetAll();
            if (list.Contains(instance))
                return;

            list.Add(instance);
            await Save(list);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task Remove(T instance)
    {
        await _semaphore.WaitAsync();
        try
        {
            var list = await GetAll();
            list.Remove(instance);
            await Save(list);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<T>> GetAll()
    {
        var jsonList = await _localStorageService.GetItemAsync<List<string>>(_storageKey)
                       ?? new List<string>();

        var result = jsonList
            .Select(Deserialize)
            .ToList();

        return result;
    }

    private static T Deserialize(string json)
    {
        return JsonSerializer.Deserialize<T>(json) ??
               throw new InvalidOperationException(
                   $"couldn't deserialize into type {typeof(T).Name} from given json: \n{json}");
    }

    private async Task Save(IEnumerable<T> instanceList)
    {
        var jsonList = instanceList.Select(x => JsonSerializer.Serialize(x)).ToArray();
        await _localStorageService.SetItemAsync(_storageKey, jsonList);
    }
}