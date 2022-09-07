using BF2TV.WindowsApp.Properties;
using MediatR;

namespace BF2TV.WindowsApp.Infrastructure.Settings;

public class AutoStartSetting : IAutoStartSetting
{
    private const string SettingKey = "ShouldStartWithWindows";
    private readonly IMediator _mediator;

    public AutoStartSetting(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool? Value
    {
        get
        {
            try
            {
                var value = UserConfiguration.Default[SettingKey];
                if (value == null)
                    return null;

                if (value is not string readOnlySpan)
                {
                    return null;
                }

                return bool.Parse(readOnlySpan);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    public void ChangeValueAsync(bool newValue)
    {
        UserConfiguration.Default[SettingKey] = newValue;
        UserConfiguration.Default.Save();
    }
}