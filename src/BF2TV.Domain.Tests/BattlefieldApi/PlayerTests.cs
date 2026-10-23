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

    [Test]
    public void ProfileUrl_IncludesEncodedNick()
    {
        var player = new Player
        {
            Pid = 123,
            Name = "Player Name+&?",
            Tag = string.Empty
        };

        Assert.That(player.ProfileUrl,
            Is.EqualTo("https://playerpath.link/p/123?nick=Player%20Name%2B%26%3F&tag="));
    }

    [Test]
    public void ProfileUrl_IsNullForZeroPid()
    {
        var player = new Player
        {
            Pid = 0,
            Name = "Player"
        };

        Assert.That(player.ProfileUrl, Is.Null);
    }
}
