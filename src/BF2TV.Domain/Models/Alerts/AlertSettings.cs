namespace BF2TV.Domain.Models.Alerts;

public class AlertSettings : IEquatable<AlertSettings>
{
    public bool IsEnabled { get; set; }

    public bool Equals(AlertSettings? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsEnabled == other.IsEnabled;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AlertSettings) obj);
    }

    public override int GetHashCode()
    {
        return IsEnabled.GetHashCode();
    }
}