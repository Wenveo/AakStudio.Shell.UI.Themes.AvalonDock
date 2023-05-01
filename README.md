# About
**The code in this repository was in one of my private projects, which is no longer in development. I then reorganized some of the code (which I thought was valuable) and made it open source.**

**For some reason, I won't be maintaining this repository long term, but you're still free to copy and use the files.**

- [AakStudio.Shell.UI](https://github.com/Wenveo/AakStudio.Shell.UI)
- [AakStudio.Shell.UI.Themes.AvalonDock](https://github.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock)

# AakStudio.Shell.UI.Themes.AvalonDock

***AakStudio.Shell.UI.Themes package for AvalonDock.***

![NetFramework](https://img.shields.io/badge/.Net%20Framework->=4.6.2-green) ![NetCore](https://img.shields.io/badge/.Net%20Core->=v3.1-blue)
[![CodeQL](https://github.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/actions/workflows/codeql.yml/badge.svg)](https://github.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/actions/workflows/codeql.yml)

![Downloads](https://img.shields.io/nuget/dt/AakStudio.Shell.UI.Themes.AvalonDock) ![PackageVersion](https://img.shields.io/nuget/v/AakStudio.Shell.UI.Themes.AvalonDock)

 [![Bilibili](https://img.shields.io/badge/dynamic/json?color=ff69b4&label=bilibili&query=%24.data.totalSubs&url=https%3A%2F%2Fapi.spencerwoo.com%2Fsubstats%2F%3Fsource%3Dbilibili%26queryKey%3D176863848)](https://space.bilibili.com/176863848)

- Visual Studio 2019 Blue
- Visual Studio 2019 Dark
- Visual Studio 2019 Light
- Visual Studio 2022 Blue
- Visual Studio 2022 Dark
- Visual Studio 2022 Light

# 💡 Install
- Install in Visual Studio using NuGet Package Manager.
- .NET CLI : `dotnet add package AakStudio.Shell.UI.Themes.AvalonDock`

# 🚀 Quick Start
**Add the namespace in xaml**
`xmlns:aakthemes="http://aakstudio.themes.avalondock"`

**Apply the theme in DockingManger**
``` xml
<DockingManager>
    <DockingManager.Theme>
        <aakthemes:VisualStudio2022Dark />
    </DockingManager.Theme>
</DockingManger>
```

**Or Merge ResourceDictionary**
``` xml
<!-- Visual Studio 2019 Blue-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/BlueTheme.xaml" />

<!-- Visual Studio 2019 Dark-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/DarkTheme.xaml" />

<!-- Visual Studio 2019 Light-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/LightTheme.xaml" />

<!-- Visual Studio 2022 Blue-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/BlueTheme.xaml" />

<!-- Visual Studio 2022 Dark-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/DarkTheme.xaml" />

<!-- Visual Studio 2022 Light-->
<ResourceDictionary Source="/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/LightTheme.xaml" />
```

# ✨ Styles and Templates
*All **AvalonDock Control's** Styles and Templates have an x:Key*.

*So you can use **BasedOn** in **Style**, And change the properties of the control's style.* (See [App.xaml](https://github.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/blob/main/DockingDemo/App.xaml))

Then we change the properties of the **Document Floating Window** to allow minimization and show in **TaskBar** and separate from the **MainWindow**.
``` xml
<Style x:Key="CustomizeDocumentFloatingWindowStyle" TargetType="{x:Type LayoutDocumentFloatingWindowControl}">
    <Setter Property="AllowMinimize" Value="True" />
    <Setter Property="ShowInTaskbar" Value="True" />
    <Setter Property="OwnedByDockingManagerWindow" Value="False" />
</Style>

<Style x:Key="{x:Type LayoutDocumentFloatingWindowControl}" TargetType="{x:Type LayoutDocumentFloatingWindowControl}">
    <Setter Property="local:DynamicStyleProvider.BasedOn" Value="{DynamicResource DocumentWellWindowBaseStyle}" />
    <Setter Property="local:DynamicStyleProvider.Derived" Value="{DynamicResource CustomizeDocumentFloatingWindowStyle}" />
</Style>
```

And we can change the **ItemTemplate** of **NavigatorWindow's List**.
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
![NavWindowListItemTemplate](https://raw.githubusercontent.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/main/Screenshots/NavWindowListItemTemplate.png)


***See for other styles [AakStudio.Shell.UI.Themes.AvalonDock/Styles](https://github.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/tree/main/AakStudio.Shell.UI.Themes.AvalonDock/Styles)***

# 📷 Screenshots
**(From [AakStudio.Shell.UI.Showcase](https://github.com/Wenveo/AakStudio.Shell.UI))**

![Screenshot1](https://raw.githubusercontent.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/main/Screenshots/1.png)

![Screenshot2](https://raw.githubusercontent.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/main/Screenshots/2.png)

![Screenshot3](https://raw.githubusercontent.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/main/Screenshots/3.png)

![Screenshot4](https://raw.githubusercontent.com/Wenveo/AakStudio.Shell.UI.Themes.AvalonDock/main/Screenshots/4.png)

# 📢 Known Problems
Custom styles and templates don't work when using themes in **DockingManager**.
``` xml
<Style x:Key="CustomizeDocumentFloatingWindowStyle" TargetType="{x:Type LayoutDocumentFloatingWindowControl}">
    <Setter Property="AllowMinimize" Value="True" />
    <Setter Property="ShowInTaskbar" Value="True" />
    <Setter Property="OwnedByDockingManagerWindow" Value="False" />
</Style>

<Style x:Key="{x:Type LayoutDocumentFloatingWindowControl}" TargetType="{x:Type LayoutDocumentFloatingWindowControl}">
    <Setter Property="local:DynamicResourceStyle.BasedOn" Value="{DynamicResource DocumentWellWindowBaseStyle}" />
    <Setter Property="local:DynamicResourceStyle.Derived" Value="{DynamicResource CustomizeDocumentFloatingWindowStyle}" />
</Style>

<!-- If you set the theme in DockingManager. Then the above style will not work -->
<DockingManager>
    <DockingManager.Theme>
        <aakthemes:VisualStudio2022Dark />
    </DockingManager.Theme>
</DockingManger>
```


#  LICENSE
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)



Email: Wenveo@outlook.com
