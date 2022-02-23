using Fluxor;

namespace BF2Dashboard.UI.Store;

public class FavoriteServerListFeature : Feature<FavoriteServerListState>
{
    public override string GetName() => nameof(FavoriteServerListFeature);

    protected override FavoriteServerListState GetInitialState() => new(null);
}