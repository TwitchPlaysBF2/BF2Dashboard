using System.Diagnostics;
using BF2TV.Domain.Models;
using BF2TV.Domain.Services;
using GameDataReader.Battlefield2.Reader;
using Microsoft.Extensions.Logging;

namespace BF2TV.WindowsApp.Services;

public class AppBasedActivePlayerLookupService : IActivePlayerLookupService
{
    private readonly IBf2DataReader _bf2DataReader;

    public AppBasedActivePlayerLookupService(IBf2DataReader bf2DataReader)
    {
        _bf2DataReader = bf2DataReader;
    }

    public BasicPlayerModel? GetPlayer()
    {
        Bf2Player player;
        try
        {
            player = _bf2DataReader.ReadActivePlayer();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Debugger.Break();
            return null;
        }

        return MapPlayer(player);
    }

    private static BasicPlayerModel MapPlayer(Bf2Player bf2Player) =>
        new(PlayerName: bf2Player.OnlineName, Prefix: bf2Player.ClanTag);
}