using System;

namespace AvalonDock.Themes.VisualStudio
{
    public static class WindowHepler
    {
        public static bool IsWin11_Or_Latest => Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= 22000;
    }
}
