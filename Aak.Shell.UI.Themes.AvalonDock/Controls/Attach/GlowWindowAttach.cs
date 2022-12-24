using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Aak.Shell.UI.Themes.AvalonDock.Controls.Attach
{
    public enum GlowMode
    {
        // Default Value
        None,
        ControlzEx
    }

    public sealed class GlowWindowAttach
    {
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
                var glowMode = GetGlowMode(window);

                if (glowMode == GlowMode.ControlzEx)
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

                var oldGlowMode = (GlowMode)e.OldValue;
                if (oldGlowMode == GlowMode.ControlzEx)
                {
                    behaviors.Remove(GetGlowWindowBehavior(behaviors));
                }


                var newGlowMode = (GlowMode)e.NewValue;
                if (newGlowMode == GlowMode.ControlzEx)
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
                    GlowDepth = 1,
                    IsGlowTransitionEnabled = true
                };
                behaviors.Add(glowWindowBehavior);
            }
            return glowWindowBehavior;
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
