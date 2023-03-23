namespace BF2TV.Domain.Services;

public interface IDateTimeProvider
{
    DateTime NowUtc { get; }
}