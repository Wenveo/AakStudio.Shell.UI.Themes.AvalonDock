using System.Windows;
using System.Windows.Media;

namespace AakStudio.Shell.UI.Themes.AvalonDock.Attachs
{
    public sealed class CustomizeGlowWindowAttach
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool),
                typeof(CustomizeGlowWindowAttach), new FrameworkPropertyMetadata(false, OnIsActiveChangedCallback));

        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.RegisterAttached("GlowBrush", typeof(SolidColorBrush),
                typeof(CustomizeGlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnGlowBrushChangedCallback));

        public static readonly DependencyProperty NonActiveGlowBrushProperty =
            DependencyProperty.RegisterAttached("NonActiveGlowBrush", typeof(SolidColorBrush),
                typeof(CustomizeGlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnGlowBrushChangedCallback));

        private static void OnIsActiveChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is bool b)
            {
                UpdateGlowBrush(window, b);
            }
        }

        private static void OnGlowBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                UpdateGlowBrush(window, GetIsActive(window));
            }
        }

        private static void UpdateGlowBrush(Window window, bool isActive)
        {
            var brush = isActive ? GetGlowBrush(window) : GetNonActiveGlowBrush(window);

            var glowWindowBehavior = window.GetOrAddBehavior(BehaviorFactory.CreateGlowWindowBehavior);
            glowWindowBehavior.GlowColor = brush.Color;
            glowWindowBehavior.NonActiveGlowColor = brush.Color;
        }

        public static bool GetIsActive(DependencyObject obj)
        => (bool)obj.GetValue(IsActiveProperty);

        public static void SetIsActive(DependencyObject obj, bool value)
        => obj.SetValue(IsActiveProperty, value);

        public static SolidColorBrush GetGlowBrush(DependencyObject obj)
        => (SolidColorBrush)obj.GetValue(GlowBrushProperty);

        public static void SetGlowBrush(DependencyObject obj, SolidColorBrush value)
        => obj.SetValue(GlowBrushProperty, value);

        public static SolidColorBrush GetNonActiveGlowBrush(DependencyObject obj)
        => (SolidColorBrush)obj.GetValue(NonActiveGlowBrushProperty);

        public static void SetNonActiveGlowBrush(DependencyObject obj, SolidColorBrush value)
        => obj.SetValue(NonActiveGlowBrushProperty, value);
    }
}
