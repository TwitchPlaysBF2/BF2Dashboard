using System.Threading.Tasks;
using Refit;

namespace BF2TV.Domain.DiscordApi;

public interface IDiscordRepository
{
    [Get("")]
    Task<DiscordMessage[]> GetDiscordMessages();
}