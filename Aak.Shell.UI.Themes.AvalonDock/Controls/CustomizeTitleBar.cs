using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace Aak.Shell.UI.Themes.AvalonDock.Controls
{
    public class CustomizeTitleBar : Border
    {
        public static readonly DependencyProperty IsShowSystemMenuProperty =
            DependencyProperty.Register("IsShowSystemMenu", typeof(bool),
                typeof(CustomizeTitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty IsWindowTitleBarProperty =
            DependencyProperty.Register("IsWindowTitleBar", typeof(bool),
                typeof(CustomizeTitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty ContextMenuDataContextProperty =
            DependencyProperty.Register(nameof(ContextMenuDataContext), typeof(object),
                typeof(CustomizeTitleBar), new FrameworkPropertyMetadata(null));

        [Bindable(true), Description("Gets/sets the DataContext to set for the context menu property."), Category("Menu")]
        public object ContextMenuDataContext
        {
            get => (object)GetValue(ContextMenuDataContextProperty);
            set => SetValue(ContextMenuDataContextProperty, value);
        }

        public bool IsShowSystemMenu
        {
            get { return (bool)GetValue(IsShowSystemMenuProperty); }
            set { SetValue(IsShowSystemMenuProperty, value); }
        }

        public bool IsWindowTitleBar
        {
            get { return (bool)GetValue(IsWindowTitleBarProperty); }
            set { SetValue(IsWindowTitleBarProperty, value); }
        }

        public CustomizeTitleBar()
        {
            PresentationSource.AddSourceChangedHandler(this, OnSourceChanged);

            ControlzEx.WindowChrome.SetIsHitTestVisibleInChrome(this, true);
            CoreceUpdateIsWindowTitleBar();
        }

        private void OnSourceChanged(object sender, SourceChangedEventArgs args)
        {
            var newHwndSource = (HwndSource?)args.NewSource;
            if (newHwndSource is not null)
            {
                newHwndSource.AddHook(WndProc);
            }

            var oldHwndSource = (HwndSource?)args.OldSource;
            if (oldHwndSource is not null)
            {
                oldHwndSource.RemoveHook(WndProc);
            }
        }

        protected virtual IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 165:
                    CoreceShowContextMenu();
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private void CoreceUpdateIsWindowTitleBar()
        {
            if (IsWindowTitleBar)
            {
                ControlzEx.Behaviors.NonClientControlProperties.SetHitTestResult(this, ControlzEx.Native.HT.CAPTION);
            }
            else
            {
                ControlzEx.Behaviors.NonClientControlProperties.SetHitTestResult(this, ControlzEx.Native.HT.TOP);
            }
        }

        private void CoreceShowContextMenu()
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }

            if (Window.GetWindow(this) is Window wnd)
            {
                if (IsShowSystemMenu)
                {
#pragma warning disable CS0618
                    ControlzEx.SystemCommands.ShowSystemMenu(wnd, PointFromScreen(MouseHelper.GetMousePosition()));
#pragma warning restore CS0618
                }
                else
                {
                    if (ContextMenu is not null)
                    {
                        ContextMenu.DataContext = ContextMenuDataContext;
                        ContextMenu.IsOpen = true;
                    }
                }
            }
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                CoreceShowContextMenu();
                e.Handled = true;
            }
        }
    }
}
