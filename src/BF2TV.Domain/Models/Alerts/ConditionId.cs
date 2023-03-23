namespace BF2TV.Domain.Models.Alerts;

public class ConditionId : IEquatable<ConditionId>
{
    public string Id { get; }

    public override string ToString() => Id;

    private ConditionId(string id)
    {
        Id = id;
    }

    public static ConditionId Create(FriendIsOnServerCondition condition)
    {
        var id = GenerateLiteral<FriendIsOnServerCondition>(condition.FriendIdentity);
        return new ConditionId(id);
    }

    public static ConditionId Create<T>(string uniqueConditionContent)
        where T : IServerCondition
    {
        var id = GenerateLiteral<T>(uniqueConditionContent);
        return new ConditionId(id);
    }

    private static string GenerateLiteral<T>(string uniqueConditionContent)
        where T : IServerCondition
    {
        return $"{nameof(ConditionId)}__{typeof(T).Name}__{uniqueConditionContent}";
    }

    public bool Equals(ConditionId? other) => other?.Id == Id;
}