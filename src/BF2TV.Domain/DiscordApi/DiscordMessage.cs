using System.Text.Json.Serialization;

namespace BF2TV.Domain.DiscordApi;

/// <summary>
/// Model generated from Discord API response: https://discord.com/api/v10/channels/{channelId}/messages
/// https://discord.com/developers/docs/resources/channel#get-channel-messages
/// </summary>
public class DiscordMessage
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("channel_id")]
    public string ChannelId { get; set; }

    [JsonPropertyName("author")]
    public Author Author { get; set; }

    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; } = new List<object>();

    [JsonPropertyName("embeds")]
    public List<Embed> Embeds { get; } = new List<Embed>();

    [JsonPropertyName("mentions")]
    public List<Mention> Mentions { get; } = new List<Mention>();

    [JsonPropertyName("mention_roles")]
    public List<object> MentionRoles { get; } = new List<object>();

    [JsonPropertyName("pinned")]
    public bool Pinned { get; set; }

    [JsonPropertyName("mention_everyone")]
    public bool MentionEveryone { get; set; }

    [JsonPropertyName("tts")]
    public bool Tts { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("edited_timestamp")]
    public DateTime? EditedTimestamp { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    [JsonPropertyName("components")]
    public List<object> Components { get; } = new List<object>();

    [JsonPropertyName("message_reference")]
    public MessageReference MessageReference { get; set; }

    [JsonPropertyName("referenced_message")]
    public ReferencedMessage ReferencedMessage { get; set; }

    [JsonPropertyName("reactions")]
    public List<Reaction> Reactions { get; } = new List<Reaction>();
}

public class Author
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("avatar_decoration")]
    public object AvatarDecoration { get; set; }

    [JsonPropertyName("discriminator")]
    public string Discriminator { get; set; }

    [JsonPropertyName("public_flags")]
    public int PublicFlags { get; set; }
}

public class Embed
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("provider")]
    public Provider Provider { get; set; }

    [JsonPropertyName("thumbnail")]
    public Thumbnail Thumbnail { get; set; }

    [JsonPropertyName("video")]
    public Video Video { get; set; }
}

public class Emoji
{
    [JsonPropertyName("id")]
    public object Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Mention
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("avatar_decoration")]
    public object AvatarDecoration { get; set; }

    [JsonPropertyName("discriminator")]
    public string Discriminator { get; set; }

    [JsonPropertyName("public_flags")]
    public int PublicFlags { get; set; }
}

public class MessageReference
{
    [JsonPropertyName("channel_id")]
    public string ChannelId { get; set; }

    [JsonPropertyName("guild_id")]
    public string GuildId { get; set; }

    [JsonPropertyName("message_id")]
    public string MessageId { get; set; }
}

public class Provider
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Reaction
{
    [JsonPropertyName("emoji")]
    public Emoji Emoji { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("me")]
    public bool Me { get; set; }
}

public class ReferencedMessage
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("channel_id")]
    public string ChannelId { get; set; }

    [JsonPropertyName("author")]
    public Author Author { get; set; }

    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; } = new List<object>();

    [JsonPropertyName("embeds")]
    public List<object> Embeds { get; } = new List<object>();

    [JsonPropertyName("mentions")]
    public List<Mention> Mentions { get; } = new List<Mention>();

    [JsonPropertyName("mention_roles")]
    public List<object> MentionRoles { get; } = new List<object>();

    [JsonPropertyName("pinned")]
    public bool Pinned { get; set; }

    [JsonPropertyName("mention_everyone")]
    public bool MentionEveryone { get; set; }

    [JsonPropertyName("tts")]
    public bool Tts { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("edited_timestamp")]
    public DateTime EditedTimestamp { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    [JsonPropertyName("components")]
    public List<object> Components { get; } = new List<object>();
}

public class Thumbnail
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}

public class Video
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}