using BF2TV.Domain.Models;
using BF2TV.Domain.Services;

namespace BF2TV.BlazorWasm.Services;

public class WebBasedActivePlayerLookupService : IActivePlayerLookupService
{
    /// <summary>
    /// PLANNED: Ask user about their player name, or read it from a user-configurable setting.
    /// Web: Since websites cannot read the local game files, it's not possible to automatically detect their player name.
    /// App: See other implementation of <see cref="IActivePlayerLookupService"/>
    /// </summary>
    public BasicPlayerModel GetPlayer()
    {
        throw new NotImplementedException();
    }
}