using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public interface IServerProvider
{
    Server GetByGuid(string serverGuid);
}