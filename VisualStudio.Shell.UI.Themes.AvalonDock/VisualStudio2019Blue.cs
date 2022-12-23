using AvalonDock.Themes;
using System;

namespace VisualStudio.Shell.UI.Themes.AvalonDock
{
    public class VisualStudio2019Blue : Theme
    {
        public override Uri GetResourceUri() =>
        new("/VisualStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/BlueTheme.xaml", UriKind.Relative);
    }
}
