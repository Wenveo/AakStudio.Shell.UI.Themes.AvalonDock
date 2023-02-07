using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using AvalonDock.Controls;

namespace AakStudio.Shell.UI.Themes.AvalonDock.Controls
{
    public enum DockTarget
    {
        Center,
        SplitLeft,
        SplitTop,
        SplitRight,
        SplitBottom,
        DockLeft,
        DockTop,
        DockRight,
        DockBottom
    }

    public class DockTargetButton : Button
    {
        private static readonly List<DockTargetButton> DockTargets = new();

        public static readonly DependencyProperty TargetDockProperty =
            DependencyProperty.Register(nameof(TargetDock), typeof(DockTarget),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(DockTarget.Center));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(new CornerRadius(0)));

        public static readonly DependencyProperty GlyphBorderBrushProperty =
            DependencyProperty.Register(nameof(GlyphBorderBrush), typeof(Brush),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty GlyphBackgroundProperty =
            DependencyProperty.Register(nameof(GlyphBackground), typeof(Brush),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty OuterBorderBrushProperty =
            DependencyProperty.Register(nameof(OuterBorderBrush), typeof(Brush),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty OuterBackgroundProperty =
            DependencyProperty.Register(nameof(OuterBackground), typeof(Brush),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty GlyphArrowBrushProperty =
            DependencyProperty.Register(nameof(GlyphArrowBrush), typeof(Brush),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty IsTargetedProperty =
            DependencyProperty.Register(nameof(IsTargeted), typeof(bool),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsOuterProperty =
            DependencyProperty.Register(nameof(IsOuter), typeof(bool),
                typeof(DockTargetButton), new FrameworkPropertyMetadata(false));

        public DockTarget TargetDock
        {
            get => (DockTarget)GetValue(TargetDockProperty);
            set => SetValue(TargetDockProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush GlyphBorderBrush
        {
            get => (Brush)GetValue(GlyphBorderBrushProperty);
            set => SetValue(GlyphBorderBrushProperty, value);
        }

        public Brush GlyphBackground
        {
            get => (Brush)GetValue(GlyphBackgroundProperty);
            set => SetValue(GlyphBackgroundProperty, value);
        }

        public Brush GlyphArrowBrush
        {
            get => (Brush)GetValue(GlyphArrowBrushProperty);
            set => SetValue(GlyphArrowBrushProperty, value);
        }

        public Brush OuterBorderBrush
        {
            get => (Brush)GetValue(OuterBorderBrushProperty);
            set => SetValue(OuterBorderBrushProperty, value);
        }

        public Brush OuterBackground
        {
            get => (Brush)GetValue(OuterBackgroundProperty);
            set => SetValue(OuterBackgroundProperty, value);
        }

        public bool IsTargeted
        {
            get => (bool)GetValue(IsTargetedProperty);
            set => SetValue(IsTargetedProperty, value);
        }

        public bool IsOuter
        {
            get => (bool)GetValue(IsOuterProperty);
            set => SetValue(IsOuterProperty, value);
        }

        static DockTargetButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockTargetButton), new FrameworkPropertyMetadata(typeof(DockTargetButton)));
        }

        public DockTargetButton()
        {
            Loaded += DockTargetButton_Loaded;
            Unloaded += DockTargetButton_Unloaded;
        }

        private void DockTargetButton_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_previewBox != null)
            {
                _previewBox.IsVisibleChanged -= Element_IsVisibleChanged;
                _previewBox = null;
            }
            DockTargets.Remove(this);
        }

        private static Path? _previewBox;
        private static DockTargetButton? _current;

        private void DockTargetButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (_previewBox == null)
            {
                // Initialize
                if (sender is DockTargetButton dockTargetButton && dockTargetButton.TemplatedParent is OverlayWindow overlayWindow)
                {
                    if (overlayWindow.Template.FindName("PART_PreviewBox", overlayWindow) is Path element)
                    {
                        _previewBox = element;
                        _previewBox.IsVisibleChanged += Element_IsVisibleChanged;
                    }
                }
            }

            DockTargets.Add(this);
        }

        private static void Element_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            if (value == true)
            {
                foreach (var item in DockTargets)
                {
                    var pos = item.PointFromScreen(MouseHelper.GetMousePosition());
                    var size = item.RenderSize;
                    // 2022.10.27 - Fix the size to show
                    size.Width += 2;
                    size.Height += 2;

                    if (new Rect(new Point(), size).Contains(pos))
                    {
                        _current = item;
                        _current.IsTargeted = true;
                        return;
                    }
                }
            }
            if (value == false && _current != null)
            {
                _current.IsTargeted = false;
                _current = null;
            }
        }
    }
}
