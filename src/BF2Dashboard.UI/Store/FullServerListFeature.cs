using Fluxor;

namespace BF2Dashboard.UI.Store;

public class FullServerListFeature : Feature<FullServerListState>
{
    public override string GetName() => nameof(FullServerListFeature);

    protected override FullServerListState GetInitialState() => new(null);
}