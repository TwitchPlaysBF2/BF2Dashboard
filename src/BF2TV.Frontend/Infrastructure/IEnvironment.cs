namespace BF2TV.Frontend.Infrastructure;

public interface IEnvironment
{
    bool IsApp();
    bool IsWeb();
}