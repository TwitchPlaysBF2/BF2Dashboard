using BF2TV.Frontend.Infrastructure;

namespace BF2TV.BlazorWasm.Infrastructure;

public class BlazorWasmEnvironment : IEnvironment
{
    public bool IsWeb() => true;
    public bool IsApp() => !IsWeb();
}