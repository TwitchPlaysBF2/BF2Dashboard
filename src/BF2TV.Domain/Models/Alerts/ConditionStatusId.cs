using System.Text;

namespace BF2TV.Domain.Models.Alerts;

public record ConditionStatusId(string Id)
{
    public override string ToString() => Id;

    public static ConditionStatusId Create(FrameIsOnServerStatus condition)
    {
        var id = GenerateUniqueLiteral<FrameIsOnServerStatus>(condition.PlayerName, condition.ServerInfo.ServerName);
        return new ConditionStatusId(id);
    }

    public static ConditionStatusId Create<T>(string uniqueConditionContent)
        where T : IConditionStatus
    {
        var id = GenerateUniqueLiteral<T>(uniqueConditionContent);
        return new ConditionStatusId(id);
    }

    private static string GenerateUniqueLiteral<T>(params string[] uniqueSetOfConditionContexts)
        where T : IConditionStatus
    {
        var builder = new StringBuilder();

        var conditionType = typeof(T).Name;
        var prefix = $"{nameof(ConditionStatusId)}__{conditionType}";
        builder.Append(prefix);

        foreach (var context in uniqueSetOfConditionContexts)
        {
            builder.Append("__");
            builder.Append(context);
        }

        return builder.ToString();
    }
}