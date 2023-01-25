using AvalonDock.Themes;

using System;

namespace AakStudio.Shell.UI.Themes.AvalonDock
{
    public class VisualStudio2019Light : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/LightTheme.xaml", UriKind.Relative);
    }
}
