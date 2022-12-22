using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;

namespace AvalonDock.Themes.VisualStudio.Controls.Attach
{
    public sealed class WindowChromeAttach
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled", typeof(bool), typeof(WindowChromeAttach), new PropertyMetadata(false, OnIsEnabledChanged));

        public static readonly DependencyProperty CornerPreferenceProperty =
            DependencyProperty.RegisterAttached(
                "CornerPreference", typeof(WindowCornerPreference), typeof(WindowChromeAttach), new PropertyMetadata(OnCornerPreferenceChanged));

        public static readonly DependencyProperty EnableMaxRestoreProperty =
            DependencyProperty.RegisterAttached(
                "EnableMaxRestore", typeof(bool), typeof(WindowChromeAttach), new PropertyMetadata(OnEnableMaxRestoreChanged));

        public static readonly DependencyProperty EnableMinimizeProperty =
            DependencyProperty.RegisterAttached(
                "EnableMinimize", typeof(bool), typeof(WindowChromeAttach), new PropertyMetadata(OnEnableMinimizeChanged));

        public static readonly DependencyProperty IgnoreTaskbarOnMaximizeProperty =
            DependencyProperty.RegisterAttached(
                "IgnoreTaskbarOnMaximize", typeof(bool), typeof(WindowChromeAttach), new PropertyMetadata(OnIgnoreTaskbarOnMaximizeChanged));

        public static readonly DependencyProperty KeepBorderOnMaximizeProperty =
            DependencyProperty.RegisterAttached(
                "KeepBorderOnMaximize", typeof(bool), typeof(WindowChromeAttach), new PropertyMetadata(OnKeepBorderOnMaximizeChanged));

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.RegisterAttached(
                "ResizeBorderThickness", typeof(Thickness), typeof(WindowChromeAttach), new PropertyMetadata(OnResizeBorderThicknessChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                var behaviors = Interaction.GetBehaviors(window);

                if ((bool)e.NewValue)
                {
                    var typeName = window.GetType().FullName;
                    if (typeName.StartsWith("AvalonDock.Controls"))
                    {
                        // Clear AvalonDock WindowChrome
                        Microsoft.Windows.Shell.WindowChrome.SetWindowChrome(window, null);
                    }
                    else
                    {
                        // Clear System WindowChrome
                        System.Windows.Shell.WindowChrome.SetWindowChrome(window, null);
                    }

                    // Initialize Default Value
                    var windowChromeBehavior = GetOrAddWindowChromeBehavior(behaviors);
                    SetCornerPreference(window, windowChromeBehavior.CornerPreference);
                    SetEnableMaxRestore(window, windowChromeBehavior.EnableMaxRestore);
                    SetEnableMinimize(window, windowChromeBehavior.EnableMinimize);
                    SetIgnoreTaskbarOnMaximize(window, windowChromeBehavior.IgnoreTaskbarOnMaximize);
                    SetKeepBorderOnMaximize(window, windowChromeBehavior.KeepBorderOnMaximize);
                    SetResizeBorderThickness(window, windowChromeBehavior.ResizeBorderThickness);
                }
            }
        }

        private static void OnCornerPreferenceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).CornerPreference = (WindowCornerPreference)e.NewValue;
            }
        }

        private static void OnEnableMaxRestoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).EnableMaxRestore = (bool)e.NewValue;
            }
        }

        private static void OnEnableMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).EnableMinimize = (bool)e.NewValue;
            }
        }

        private static void OnIgnoreTaskbarOnMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).IgnoreTaskbarOnMaximize = (bool)e.NewValue;
            }
        }

        private static void OnKeepBorderOnMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).KeepBorderOnMaximize = (bool)e.NewValue;
            }
        }

        private static void OnResizeBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                GetOrAddWindowChromeBehavior(Interaction.GetBehaviors(window)).ResizeBorderThickness = (Thickness)e.NewValue;
            }
        }

        private static WindowChromeBehavior? GetWindowChromeBehavior(BehaviorCollection behaviors)
        {
            return (WindowChromeBehavior?)behaviors.FirstOrDefault(x => x.GetType() == typeof(WindowChromeBehavior));
        }

        private static WindowChromeBehavior GetOrAddWindowChromeBehavior(BehaviorCollection behaviors)
        {
            var windowChromeBehavior = GetWindowChromeBehavior(behaviors);
            if (windowChromeBehavior == null)
            {
                windowChromeBehavior ??= new WindowChromeBehavior();
                behaviors.Add(windowChromeBehavior);
            }
            return windowChromeBehavior;
        }

        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static WindowCornerPreference GetCornerPreference(DependencyObject element)
        {
            return (WindowCornerPreference)element.GetValue(CornerPreferenceProperty);
        }

        public static void SetCornerPreference(DependencyObject element, WindowCornerPreference value)
        {
            element.SetValue(CornerPreferenceProperty, value);
        }

        public static bool GetEnableMaxRestore(DependencyObject element)
        {
            return (bool)element.GetValue(EnableMaxRestoreProperty);
        }

        public static void SetEnableMaxRestore(DependencyObject element, bool value)
        {
            element.SetValue(EnableMaxRestoreProperty, value);
        }

        public static bool GetEnableMinimize(DependencyObject element)
        {
            return (bool)element.GetValue(EnableMinimizeProperty);
        }

        public static void SetEnableMinimize(DependencyObject element, bool value)
        {
            element.SetValue(EnableMinimizeProperty, value);
        }

        public static bool GetIgnoreTaskbarOnMaximize(DependencyObject element)
        {
            return (bool)element.GetValue(IgnoreTaskbarOnMaximizeProperty);
        }

        public static void SetIgnoreTaskbarOnMaximize(DependencyObject element, bool value)
        {
            element.SetValue(IgnoreTaskbarOnMaximizeProperty, value);
        }

        public static bool GetKeepBorderOnMaximize(DependencyObject element)
        {
            return (bool)element.GetValue(KeepBorderOnMaximizeProperty);
        }

        public static void SetKeepBorderOnMaximize(DependencyObject element, bool value)
        {
            element.SetValue(KeepBorderOnMaximizeProperty, value);
        }

        public static Thickness GetResizeBorderThickness(DependencyObject element)
        {
            return (Thickness)element.GetValue(ResizeBorderThicknessProperty);
        }

        public static void SetResizeBorderThickness(DependencyObject element, Thickness value)
        {
            element.SetValue(ResizeBorderThicknessProperty, value);
        }
    }
}
