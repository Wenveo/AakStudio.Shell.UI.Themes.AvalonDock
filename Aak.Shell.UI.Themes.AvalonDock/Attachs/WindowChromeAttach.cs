using ControlzEx.Behaviors;

using System.Windows;

namespace Aak.Shell.UI.Themes.AvalonDock.Attachs
{
    public sealed class WindowChromeAttach
    {
        public static readonly DependencyProperty CornerPreferenceProperty;

        public static readonly DependencyProperty EnableMaxRestoreProperty;

        public static readonly DependencyProperty EnableMinimizeProperty;

        public static readonly DependencyProperty IgnoreTaskbarOnMaximizeProperty;

        public static readonly DependencyProperty KeepBorderOnMaximizeProperty;

        public static readonly DependencyProperty ResizeBorderThicknessProperty;

        static WindowChromeAttach()
        {
            var defauleObject = BehaviorFactory.CreateWindowChromeBehavior();

            CornerPreferenceProperty =
                DependencyProperty.RegisterAttached("CornerPreference", typeof(WindowCornerPreference),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.CornerPreference,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnCornerPreferenceChangedCallback));

            EnableMaxRestoreProperty =
                DependencyProperty.RegisterAttached("EnableMaxRestore", typeof(bool),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.EnableMaxRestore,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnEnableMaxRestoreChangedCallback));

            EnableMinimizeProperty =
                DependencyProperty.RegisterAttached("EnableMinimize", typeof(bool),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.EnableMinimize,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnEnableMinimizeChangedCallback));

            IgnoreTaskbarOnMaximizeProperty =
                DependencyProperty.RegisterAttached("IgnoreTaskbarOnMaximize", typeof(bool),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.IgnoreTaskbarOnMaximize,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnIgnoreTaskbarOnMaximizeChangedCallback));

            KeepBorderOnMaximizeProperty =
                DependencyProperty.RegisterAttached("KeepBorderOnMaximize", typeof(bool),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.KeepBorderOnMaximize,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnKeepBorderOnMaximizeChangedCallback));

            ResizeBorderThicknessProperty =
                DependencyProperty.RegisterAttached("ResizeBorderThickness", typeof(Thickness),
                    typeof(WindowChromeAttach), new FrameworkPropertyMetadata(defauleObject.ResizeBorderThickness,
                    FrameworkPropertyMetadataOptions.AffectsRender, OnResizeBorderThicknessChangedCallback));
        }

        private static void OnCornerPreferenceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is WindowCornerPreference cornerPreference)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).CornerPreference = cornerPreference;
            }
        }

        private static void OnEnableMaxRestoreChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is bool b)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).EnableMaxRestore = b;
            }
        }

        private static void OnEnableMinimizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is bool b)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).EnableMinimize = b;
            }
        }

        private static void OnIgnoreTaskbarOnMaximizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is bool b)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).IgnoreTaskbarOnMaximize = b;
            }
        }

        private static void OnKeepBorderOnMaximizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is bool b)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).KeepBorderOnMaximize = b;
            }
        }

        private static void OnResizeBorderThicknessChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window && e.NewValue is Thickness thickness)
            {
                d.GetOrAddBehavior(BehaviorFactory.CreateWindowChromeBehavior).ResizeBorderThickness = thickness;
            }
        }

        public static WindowCornerPreference GetCornerPreference(DependencyObject obj)
        => (WindowCornerPreference)obj.GetValue(CornerPreferenceProperty);

        public static void SetCornerPreference(DependencyObject obj, WindowCornerPreference value)
        => obj.SetValue(CornerPreferenceProperty, value);

        public static bool GetEnableMaxRestore(DependencyObject obj)
        => (bool)obj.GetValue(EnableMaxRestoreProperty);

        public static void SetEnableMaxRestore(DependencyObject obj, bool value)
        => obj.SetValue(EnableMaxRestoreProperty, value);

        public static bool GetEnableMinimize(DependencyObject obj)
        => (bool)obj.GetValue(EnableMinimizeProperty);

        public static void SetEnableMinimize(DependencyObject obj, bool value)
        => obj.SetValue(EnableMinimizeProperty, value);

        public static bool GetIgnoreTaskbarOnMaximize(DependencyObject obj)
        => (bool)obj.GetValue(IgnoreTaskbarOnMaximizeProperty);

        public static void SetIgnoreTaskbarOnMaximize(DependencyObject obj, bool value)
        => obj.SetValue(IgnoreTaskbarOnMaximizeProperty, value);

        public static bool GetKeepBorderOnMaximize(DependencyObject obj)
        => (bool)obj.GetValue(KeepBorderOnMaximizeProperty);

        public static void SetKeepBorderOnMaximize(DependencyObject obj, bool value)
        => obj.SetValue(KeepBorderOnMaximizeProperty, value);

        public static Thickness GetResizeBorderThickness(DependencyObject obj)
        => (Thickness)obj.GetValue(ResizeBorderThicknessProperty);

        public static void SetResizeBorderThickness(DependencyObject obj, Thickness value)
        => obj.SetValue(ResizeBorderThicknessProperty, value);
    }
}
