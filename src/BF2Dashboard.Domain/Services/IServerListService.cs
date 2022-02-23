using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.Domain.Services;

public interface IServerListService
{
    Task<List<Server>> GetServerList();
}