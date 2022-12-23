using AvalonDock.Themes;
using System;

namespace VisualStudio.Shell.UI.Themes.AvalonDock
{
    public class VisualStudio2019Light : Theme
    {
        public override Uri GetResourceUri() =>
        new("/VisualStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/LightTheme.xaml", UriKind.Relative);
    }
}
