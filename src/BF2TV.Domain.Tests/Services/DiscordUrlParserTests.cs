using BF2TV.Domain.Services;
using FluentAssertions;
using NUnit.Framework;

namespace BF2TV.Domain.Tests.Services;

public class DiscordUrlParserTests
{
    private DiscordUrlParser _sut = new();

    [Ignore("Ignored until GitHub Issue #37 is fixed")]
    [TestCase("Join Us On Discord https://discord.gg/RB7PjBk", ExpectedResult = "https://discord.gg/RB7PjBk")]
    [TestCase("https://www.discord.gg/YUkd6RKa6e", ExpectedResult = "https://www.discord.gg/YUkd6RKa6e")]
    [TestCase("https://discord.gg/YUkd6RKa6e", ExpectedResult = "https://discord.gg/YUkd6RKa6e")]
    [TestCase("http://discord.gg/YUkd6RKa6e", ExpectedResult = "http://discord.gg/YUkd6RKa6e")]
    [TestCase("discord.gg/YUkd6RKa6e", ExpectedResult = "discord.gg/YUkd6RKa6e")]
    [TestCase("https://discord.com/invite/RB7PjBk", ExpectedResult = "https://discord.com/invite/RB7PjBk")]
    [TestCase("Join Discord https://discord.gg/YUkd6RKa6e and visit www.bf2.tv",
        ExpectedResult = "https://discord.gg/YUkd6RKa6e")]
    public string TryGetDiscordUrl_ShouldRecognizeDiscordUrl_WhenTextHasDiscordUrl(string textToParse)
    {
        var success = _sut.TryGetDiscordUrl(textToParse, out var discordUrl);

        success.Should().BeTrue(because: "regex is matching the rule");
        return discordUrl;
    }
}