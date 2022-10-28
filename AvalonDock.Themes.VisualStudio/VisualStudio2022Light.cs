using System;

namespace AvalonDock.Themes.VisualStudio
{
    public class VisualStudio2022Light : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AvalonDock.Themes.VisualStudio;component/Themes/VisualStudio2022/LightTheme.xaml", UriKind.Relative);
    }
}
