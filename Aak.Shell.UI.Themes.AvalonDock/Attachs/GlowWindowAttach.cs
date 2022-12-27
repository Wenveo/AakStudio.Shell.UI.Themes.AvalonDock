using System.Windows;
using System.Windows.Media;

namespace Aak.Shell.UI.Themes.AvalonDock.Attachs
{
    public sealed class GlowWindowAttach
    {
        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.RegisterAttached("GlowBrush", typeof(SolidColorBrush),
                typeof(GlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnGlowBrushChangedCallback));

        public static readonly DependencyProperty NonActiveGlowBrushProperty =
            DependencyProperty.RegisterAttached("NonActiveGlowBrush", typeof(SolidColorBrush),
                typeof(GlowWindowAttach), new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnNonActiveGlowBrushChangedCallback));

        private static void OnGlowBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is SolidColorBrush brush)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateGlowWindowBehavior).GlowColor = brush.Color;
            }
        }

        private static void OnNonActiveGlowBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is SolidColorBrush brush)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateGlowWindowBehavior).NonActiveGlowColor = brush.Color;
            }
        }

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