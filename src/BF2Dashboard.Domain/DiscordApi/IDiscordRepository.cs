using Refit;

namespace BF2Dashboard.Domain.DiscordApi;

public interface IDiscordRepository
{
    [Get("")]
    Task<DiscordMessage[]> GetDiscordMessages();
}