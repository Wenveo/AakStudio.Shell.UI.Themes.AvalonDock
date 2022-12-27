using System.Windows;
using System.Windows.Media;

namespace Aak.Shell.UI.Themes.AvalonDock.Attachs
{
    public sealed class CustomizeGlowWindowAttach
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool),
                typeof(CustomizeGlowWindowAttach), new PropertyMetadata(false, OnIsActiveChangedCallback));

        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.RegisterAttached("GlowBrush", typeof(SolidColorBrush),
                typeof(CustomizeGlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty NonActiveGlowBrushProperty =
            DependencyProperty.RegisterAttached("NonActiveGlowBrush", typeof(SolidColorBrush),
                typeof(CustomizeGlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        private static void OnIsActiveChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is bool b)
            {
                var brush = b ? GetGlowBrush(d) : GetNonActiveGlowBrush(d);

                d.GetOrAddBehavior(BehaviorFactory.CreateGlowWindowBehavior).GlowColor = brush.Color;
                d.GetOrAddBehavior(BehaviorFactory.CreateGlowWindowBehavior).NonActiveGlowColor = brush.Color;
            }
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
