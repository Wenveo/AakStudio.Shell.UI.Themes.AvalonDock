using AvalonDock.Themes;

using System;

namespace AakStudio.Shell.UI.Themes.AvalonDock
{
    public class VisualStudio2019Dark : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/DarkTheme.xaml", UriKind.Relative);
    }
}
