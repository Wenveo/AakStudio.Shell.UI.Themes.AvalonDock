using AvalonDock.Themes.VisualStudio.Behaviors;
using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AvalonDock.Themes.VisualStudio.Controls.Attach
{
    public enum GlowMode
    {
        // Default Value
        None,
        Auto,
        Custom,
        ControlzEx
    }

    public sealed class GlowWindowAttach
    {
        private static bool IsWin11_Or_Latest => Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= 22000;

        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.RegisterAttached(
                "GlowBrush", typeof(SolidColorBrush), typeof(GlowWindowAttach), new PropertyMetadata(Brushes.Transparent, OnGlowBrushChanged));

        public static readonly DependencyProperty GlowModeProperty =
            DependencyProperty.RegisterAttached(
                "GlowMode", typeof(GlowMode), typeof(GlowWindowAttach), new PropertyMetadata(GlowMode.None, OnGlowModeChanged));

        private static void OnGlowBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                var behaviors = Interaction.GetBehaviors(window);
                var glowMode = GetDefaultMode(GetGlowMode(window));

                if (glowMode == GlowMode.Custom)
                {
                    var visualStudioGlowWindowBehavior = GetOrAddCustomGlowWindowBehavior(behaviors);
                    visualStudioGlowWindowBehavior.ActiveGlowBrush = GetGlowBrush(d);
                    visualStudioGlowWindowBehavior.InactiveGlowBrush = GetGlowBrush(d);
                }
                else if (glowMode == GlowMode.ControlzEx)
                {
                    var glowWindowBehavior = GetOrAddGlowWindowBehavior(behaviors);
                    glowWindowBehavior.GlowColor = GetGlowBrush(d).Color;
                    glowWindowBehavior.NonActiveGlowColor = GetGlowBrush(d).Color;
                }
                
            }
        }

        private static void OnGlowModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                var behaviors = Interaction.GetBehaviors(window);

                var oldGlowMode = GetDefaultMode((GlowMode)e.OldValue);
                if (oldGlowMode == GlowMode.Custom)
                {
                    behaviors.Remove(GetCustomGlowWindowBehavior(behaviors));
                }
                else if (oldGlowMode == GlowMode.ControlzEx)
                {
                    behaviors.Remove(GetGlowWindowBehavior(behaviors));
                }


                var newGlowMode = GetDefaultMode((GlowMode)e.NewValue);
                if (newGlowMode == GlowMode.Custom)
                {
                    GetOrAddCustomGlowWindowBehavior(behaviors);
                }
                else if (newGlowMode == GlowMode.ControlzEx)
                {
                    GetOrAddGlowWindowBehavior(behaviors);
                }
            }
        }

        private static GlowWindowBehavior? GetGlowWindowBehavior(BehaviorCollection behaviors)
        {
            return (GlowWindowBehavior?)behaviors.FirstOrDefault(x => x.GetType() == typeof(GlowWindowBehavior));
        }

        private static GlowWindowBehavior GetOrAddGlowWindowBehavior(BehaviorCollection behaviors)
        {
            var glowWindowBehavior = GetGlowWindowBehavior(behaviors);
            if (glowWindowBehavior == null)
            {
                glowWindowBehavior ??= new GlowWindowBehavior()
                {
                    GlowDepth = 1
                };
                behaviors.Add(glowWindowBehavior);
            }
            return glowWindowBehavior;
        }

        private static VisualStudioGlowWindowBehavior? GetCustomGlowWindowBehavior(BehaviorCollection behaviors)
        {
            return (VisualStudioGlowWindowBehavior?)behaviors.FirstOrDefault(x => x.GetType() == typeof(VisualStudioGlowWindowBehavior));
        }

        private static VisualStudioGlowWindowBehavior GetOrAddCustomGlowWindowBehavior(BehaviorCollection behaviors)
        {
            var glowWindowBehavior = GetCustomGlowWindowBehavior(behaviors);
            if (glowWindowBehavior == null)
            {
                glowWindowBehavior ??= new VisualStudioGlowWindowBehavior()
                {
                    DoNotUseTimerTick = true
                };
                behaviors.Add(glowWindowBehavior);
            }

            return glowWindowBehavior;
        }

        private static GlowMode GetDefaultMode(GlowMode glowMode)
        {
            return glowMode == GlowMode.Auto ? (IsWin11_Or_Latest ? GlowMode.ControlzEx : GlowMode.Custom) : glowMode;
        }

        public static SolidColorBrush GetGlowBrush(DependencyObject element)
        {
            return (SolidColorBrush)element.GetValue(GlowBrushProperty);
        }

        public static void SetGlowBrush(DependencyObject element, SolidColorBrush value)
        {
            element.SetValue(GlowBrushProperty, value);
        }

        public static GlowMode GetGlowMode(DependencyObject element)
        {
            return (GlowMode)element.GetValue(GlowModeProperty);
        }

        public static void SetGlowMode(DependencyObject element, GlowMode value)
        {
            element.SetValue(GlowModeProperty, value);
        }
    }
}
