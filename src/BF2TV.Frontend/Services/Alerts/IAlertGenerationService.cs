using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Frontend.Services.Alerts;

public interface IAlertGenerationService
{
    void Generate(List<Server> fullServerList);
}