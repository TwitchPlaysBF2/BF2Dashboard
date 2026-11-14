using BF2TV.Domain.BattlefieldApi;
using NUnit.Framework;

namespace BF2TV.Domain.Tests.BattlefieldApi;

public class PlayerTests
{
    [TestCase("human")]
    public void IsHuman_KindHuman_IsHuman(string kind)
    {
        var player = new Player
        {
            Kind = kind
        };

        Assert.That(player.IsHuman, Is.True);
    }

    [TestCase("aibot")]
    [TestCase("bot")]
    [TestCase("seeder")]
    [TestCase("unknown")]
    [TestCase(null)]
    public void IsHuman_NonHumanKind_IsNotHuman(string? kind)
    {
        var player = new Player
        {
            Kind = kind,
            Aibot = false,
            Score = 1,
            Ping = 1
        };

        Assert.That(player.IsHuman, Is.False);
    }    [Test]
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
