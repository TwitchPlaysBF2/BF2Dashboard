using BF2TV.Domain.Models;

namespace BF2TV.Domain.Services;

public interface IActivePlayerLookupService
{
    BasicPlayerModel? GetPlayer();
}