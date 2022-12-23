using AvalonDock.Themes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VisualStudio.Shell.UI.Themes.AvalonDock;
using VisualStudio.Shell.UI.Themes.AvalonDock.Controls.Attach;
using VisualStudio.Shell.UI.Themes.AvalonDock.Themes;

namespace DockingDemo;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private static readonly List<Theme> Themes = new()
        {
            new VisualStudio2019Blue(),
            new VisualStudio2019Dark(),
            new VisualStudio2019Light(),
            new VisualStudio2022Blue(),
            new VisualStudio2022Dark(),
            new VisualStudio2022Light()
        };

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        UpdateGlowWindowBrush(true);
    }

    protected override void OnDeactivated(EventArgs e)
    {
        base.OnDeactivated(e);
        UpdateGlowWindowBrush(false);
    }

    private void UpdateGlowWindowBrush(bool isActive)
    {
        if (dockingManager.Theme != null)
        {
            var resource = new ResourceDictionary() { Source = dockingManager.Theme.GetResourceUri() };

            var brush = (SolidColorBrush?)resource.MergedDictionaries[0][isActive ? ResourceKeys.FloatingDocumentWindowBorder : ResourceKeys.FloatingDocumentWindowBorderInactive];
            if (brush != null)
            {
                GlowWindowAttach.SetGlowBrush(this, brush);
            }
        }
    }

    private void ThemeItem_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem)
        {
            dockingManager.Theme = Themes[int.Parse((string)menuItem.Tag)];
            UpdateGlowWindowBrush(true);
        }
    }
}
