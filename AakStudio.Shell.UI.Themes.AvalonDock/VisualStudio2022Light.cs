using AvalonDock.Themes;

using System;

namespace AakStudio.Shell.UI.Themes.AvalonDock
{
    public class VisualStudio2022Light : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/LightTheme.xaml", UriKind.Relative);
    }
}
