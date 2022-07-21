using BF2TV.Domain.BattlefieldApi;
using NUnit.Framework;

namespace BF2TV.Domain.Tests.BattlefieldApi;

public class PlayerTests
{
    [TestCase(false, 1, 1)]
    [TestCase(false, 1, 0)]
    [TestCase(false, 0, 1)]
    public void IsLikelyHuman_HumanSignature_IsHuman(bool isAibot, int score, int ping)
    {
        var player = new Player
        {
            Aibot = isAibot,
            Score = score,
            Ping = ping
        };

        Assert.That(player.IsHuman, Is.True);
    }

    [TestCase(true, 0, 0)]
    [TestCase(false, 0, 0)]
    public void IsLikelyHuman_BotSignature_IsNotHuman(bool isAibot, int score, int ping)
    {
        var player = new Player
        {
            Aibot = isAibot,
            Score = score,
            Ping = ping
        };

        Assert.That(player.IsHuman, Is.False);
    }
}