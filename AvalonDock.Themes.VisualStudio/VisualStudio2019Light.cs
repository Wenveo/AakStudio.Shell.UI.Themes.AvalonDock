﻿using System;

namespace AvalonDock.Themes.VisualStudio
{
    public class VisualStudio2019Light : Theme
    {
        public override Uri GetResourceUri() =>
        new("/AvalonDock.Themes.VisualStudio;component/Themes/VisualStudio2019/LightTheme.xaml", UriKind.Relative);
    }
}