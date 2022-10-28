using System;

namespace AvalonDock.Themes.VisualStudio
{
    public class VisualStudio2022Dark : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AvalonDock.Themes.VisualStudio;component/Themes/VisualStudio2022/DarkTheme.xaml", UriKind.Relative);
    }
}
