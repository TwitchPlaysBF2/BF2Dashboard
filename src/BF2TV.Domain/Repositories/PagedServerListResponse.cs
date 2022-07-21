using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Repositories;

public class PagedServerListResponse
{
    public PagedServerListResponse(IReadOnlyList<Server> servers, int currentPage, string? maximumKnownPages)
    {
        CurrentPage = currentPage;
        MaximumKnownPages = ParsePages(maximumKnownPages);
        Servers = servers;
    }

    public IReadOnlyList<Server> Servers { get; }

    public bool IsEmpty => Servers.Count == 0;

    public bool IsLastPage => CurrentPage >= MaximumKnownPages;

    private int CurrentPage { get; }

    private int? MaximumKnownPages { get; }

    private static int? ParsePages(string? pages)
    {
        if (pages == null)
            return null;

        if (!int.TryParse(pages, out var maximumKnownPages))
            return null;

        return maximumKnownPages;
    }
}