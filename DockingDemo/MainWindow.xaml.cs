using Aak.Shell.UI.Themes.AvalonDock;

using AvalonDock.Themes;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

    private void ThemeItem_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = Themes[int.Parse((string)menuItem.Tag)].GetResourceUri();
        }
    }
}
