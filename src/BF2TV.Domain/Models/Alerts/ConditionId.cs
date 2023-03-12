namespace BF2TV.Domain.Models.Alerts;

public record ConditionId(string Id)
{
    public override string ToString() => Id;

    public static ConditionId Create<TCondition>(string uniqueConditionContent)
        where TCondition : IServerCondition
    {
        var id = GenerateLiteral<TCondition>(uniqueConditionContent);
        return new ConditionId(id);
    }

    public static ConditionId Create(FriendIsOnServerCondition condition)
    {
        var id = GenerateLiteral<FriendIsOnServerCondition>(condition.FriendIdentity);
        return new ConditionId(id);
    }

    private static string GenerateLiteral<TConditionType>(string uniqueConditionContent)
        where TConditionType : IServerCondition
    {
        return $"{nameof(ConditionId)}__{typeof(TConditionType).Name}__{uniqueConditionContent}";
    }
}