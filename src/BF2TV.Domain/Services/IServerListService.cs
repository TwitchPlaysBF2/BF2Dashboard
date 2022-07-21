using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Services;

public interface IServerListService
{
    Task<List<Server>> GetServerList();
}