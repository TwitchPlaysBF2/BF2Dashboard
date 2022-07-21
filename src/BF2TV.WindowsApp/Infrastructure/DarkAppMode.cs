using System.Runtime.InteropServices;

namespace BF2TV.WindowsApp.Infrastructure;

/// <summary>
/// Black magic to make the form title bar go dark, from https://stackoverflow.com/a/64927217
/// </summary>
public static class DarkAppMode
{
    [DllImport("DwmApi")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

    public static void Enable(IntPtr intPtr)
    {
        if (DwmSetWindowAttribute(intPtr, 19, new[] { 1 }, 4) != 0)
            DwmSetWindowAttribute(intPtr, 20, new[] { 1 }, 4);
    }
}