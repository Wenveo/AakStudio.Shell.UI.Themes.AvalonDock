using AvalonDock.Themes.VisualStudio.Behaviors;
using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AvalonDock.Themes.VisualStudio.Controls.Attach
{
    public sealed class GlowWindowAttach
    {
        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.RegisterAttached(
                "GlowBrush", typeof(SolidColorBrush), typeof(GlowWindowAttach), new PropertyMetadata(Brushes.Transparent, OnGlowBrushChanged));

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled", typeof(bool), typeof(GlowWindowAttach), new PropertyMetadata(false, OnIsEnabledChanged));

        private static void OnGlowBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                var behaviors = Interaction.GetBehaviors(window);

                if (!WindowHepler.IsWin11_Or_Latest)
                {
                    var item = behaviors.FirstOrDefault(x => x.GetType() == typeof(VisualStudioGlowWindowBehavior));
                    if (item != null && item is VisualStudioGlowWindowBehavior visualStudioGlowWindowBehavior)
                    {
                        visualStudioGlowWindowBehavior.ActiveGlowBrush = GetGlowBrush(d);
                        visualStudioGlowWindowBehavior.InactiveGlowBrush = GetGlowBrush(d);
                    }
                }
                else
                {
                    var item = behaviors.FirstOrDefault(x => x.GetType() == typeof(GlowWindowBehavior));
                    if (item != null && item is GlowWindowBehavior glowWindowBehavior)
                    {
                        glowWindowBehavior.GlowColor = GetGlowBrush(d).Color;
                        glowWindowBehavior.NonActiveGlowColor = GetGlowBrush(d).Color;
                    }
                }
            }
        }
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                var value = (bool)e.NewValue;
                var behaviors = Interaction.GetBehaviors(window);

                var glowWindowBehavior = behaviors.FirstOrDefault(x => x.GetType() ==
                    (WindowHepler.IsWin11_Or_Latest ?
                    typeof(VisualStudioGlowWindowBehavior) :
                    typeof(GlowWindowBehavior)));

                if (value == true && glowWindowBehavior == null)
                {
                    if (!WindowHepler.IsWin11_Or_Latest)
                    {
                        glowWindowBehavior = new VisualStudioGlowWindowBehavior()
                        {
                            DoNotUseTimerTick = true
                        };
                    }
                    else
                    {
                        glowWindowBehavior = new GlowWindowBehavior();
                    }
                    Interaction.GetBehaviors(window).Add(glowWindowBehavior);
                    return;
                }

                if (value == false && glowWindowBehavior != null)
                {
                    behaviors.Remove(glowWindowBehavior);
                }
            }
        }


        public static SolidColorBrush GetGlowBrush(DependencyObject element)
        {
            return (SolidColorBrush)element.GetValue(GlowBrushProperty);
        }
        public static void SetGlowBrush(DependencyObject element, SolidColorBrush value)
        {
            element.SetValue(GlowBrushProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }
    }
}
