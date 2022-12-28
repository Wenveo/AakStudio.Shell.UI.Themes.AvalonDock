# Aak.Shell.UI.Themes.AvalonDock

***Aak.Shell.UI.Themes package for AvalonDock.***

![NetFramework](https://img.shields.io/badge/.Net%20Framework->=4.6.2-green) ![NetCore](https://img.shields.io/badge/.Net%20Core->=v3.1-blue)
[![CodeQL](https://github.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/actions/workflows/codeql.yml/badge.svg)](https://github.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/actions/workflows/codeql.yml)

![Downloads](https://img.shields.io/nuget/dt/Aak.Shell.UI.Themes.AvalonDock) ![PackageVersion](https://img.shields.io/nuget/v/Aak.Shell.UI.Themes.AvalonDock)

 [![Bilibili](https://img.shields.io/badge/dynamic/json?color=ff69b4&label=bilibili&query=%24.data.totalSubs&url=https%3A%2F%2Fapi.spencerwoo.com%2Fsubstats%2F%3Fsource%3Dbilibili%26queryKey%3D176863848)](https://space.bilibili.com/176863848)

- VisualStudio 2019 (Blue)
- VisualStudio 2019 (Dark)
- VisualStudio 2019 (Light)
- VisualStudio 2022 (Blue)
- VisualStudio 2022 (Dark)
- VisualStudio 2022 (Light)

# 💡 Install
- Install using NuGet Package Manager in Visual Studio.
- .NET CLI : `dotnet add package Aak.Shell.UI.Themes.AvalonDock`

# 🚀 Quick Start
**Add the namespace in xaml**
`xmlns:avalondockThemes="http://aak.shell.ui.themes.avalondock"`

**Apply the theme in DockingManger**
``` xml
<DockingManager>
    <DockingManager.Theme>
        <avalondockThemes:VisualStudio2022Dark />
    </DockingManager.Theme>
</DockingManger>
```

**Or Merge ResourceDictionary**
``` xml
<!-- Visual Studio 2019 Blue-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/BlueTheme.xaml" />

<!-- Visual Studio 2019 Dark-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/DarkTheme.xaml" />

<!-- Visual Studio 2019 Light-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/LightTheme.xaml" />

<!-- Visual Studio 2022 Blue-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/BlueTheme.xaml" />

<!-- Visual Studio 2022 Dark-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/DarkTheme.xaml" />

<!-- Visual Studio 2022 Light-->
<ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/LightTheme.xaml" />
```

# ✨ Styles and Templates
*All **AvalonDock Control's** Styles and Templates have an x:Key*.

*So you can use **BasedOn** in **Style**, And change the properties of the control's style.* (See [App.xaml](https://github.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/blob/main/DockingDemo/App.xaml))

Before doing this, you need to merge a theme **ResourceDictionary** (Any Theme).
``` xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="/Aak.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/DarkTheme.xaml" />
</ResourceDictionary.MergedDictionaries>
```
Then we change the properties of the **Document Floating Window** to allow minimization and show in **TaskBar** and separate from the **MainWindow**.
``` xml
<!-- xmlns:avalonDockControls="https://github.com/Dirkster99/AvalonDock" -->

<Style
    x:Key="{x:Type avalonDockControls:LayoutDocumentFloatingWindowControl}"
    BasedOn="{StaticResource DocumentWellWindowBaseStyle}"
    TargetType="{x:Type avalonDockControls:LayoutDocumentFloatingWindowControl}">
    <Setter Property="AllowMinimize" Value="True" />
    <Setter Property="ShowInTaskbar" Value="True" />
    <Setter Property="OwnedByDockingManagerWindow" Value="False" />
</Style>
```

And we can modify the **ItemTemplate** of **NavigatorWindow's List**.
``` xml
<DataTemplate x:Key="NavigatorWindowListBoxItemDataTemplate">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="16" />
            <ColumnDefinition Width="150" />
        </Grid.ColumnDefinitions>
        <TextBlock
            Grid.Column="1"
            Margin="4,0,0,0"
            Text="Custom ItemTemplate Test"
            TextTrimming="CharacterEllipsis" />
    </Grid>
</DataTemplate>
```
![NavWindowListItemTemplate](https://raw.githubusercontent.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/main/Screenshots/NavWindowListItemTemplate.png)


***See for other styles [Aak.Shell.UI.Themes.AvalonDock/Styles](https://github.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/tree/main/Aak.Shell.UI.Themes.AvalonDock/Styles)***

# 📷 Screenshots
**(Form [Aak.Shell.UI.Showcase](https://github.com/Noisrev/Aak.Shell.UI))**

![Screenshot1](https://raw.githubusercontent.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/main/Screenshots/1.png)

![Screenshot2](https://raw.githubusercontent.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/main/Screenshots/2.png)

![Screenshot3](https://raw.githubusercontent.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/main/Screenshots/3.png)

![Screenshot4](https://raw.githubusercontent.com/Noisrev/Aak.Shell.UI.Themes.AvalonDock/main/Screenshots/4.png)

# 📢 Known issue
Custom styles and templates don't work when using themes in **DockingManager**.
``` xml
<Style
    x:Key="{x:Type avalonDockControls:LayoutDocumentFloatingWindowControl}"
    BasedOn="{StaticResource DocumentWellWindowBaseStyle}"
    TargetType="{x:Type avalonDockControls:LayoutDocumentFloatingWindowControl}">
    <Setter Property="AllowMinimize" Value="True" />
    <Setter Property="ShowInTaskbar" Value="True" />
    <Setter Property="OwnedByDockingManagerWindow" Value="False" />
</Style>

<!-- If you set the theme in DockingManager. Then the above style will not work -->
<DockingManager>
    <DockingManager.Theme>
        <avalondockThemes:VisualStudio2022Dark />
    </DockingManager.Theme>
</DockingManger>
```

Email: Noisrev@outlook.com
