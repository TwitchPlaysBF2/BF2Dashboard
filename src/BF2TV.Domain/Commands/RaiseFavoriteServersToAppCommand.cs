using BF2TV.Domain.BattlefieldApi;
using MediatR;

namespace BF2TV.Domain.Commands;

public class RaiseFavoriteServerListToApp : IRequest
{
    public List<Server> ServerList { get; }

    public RaiseFavoriteServerListToApp(List<Server> serverList)
    {
        ServerList = serverList;
    }
}