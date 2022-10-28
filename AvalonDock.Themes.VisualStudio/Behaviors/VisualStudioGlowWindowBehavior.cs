using AvalonDock.Themes.VisualStudio.Controls.GlowWindow;
using AvalonDock.Themes.VisualStudio.Helpers;
using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace AvalonDock.Themes.VisualStudio.Behaviors
{
    public class VisualStudioGlowWindowBehavior : Behavior<Window>
    {

        public static readonly DependencyProperty ActiveGlowBrushProperty = DependencyProperty.Register(
                "ActiveGlowBrush", typeof(SolidColorBrush), typeof(VisualStudioGlowWindowBehavior),
                new PropertyMetadata(Brushes.Transparent, OnGlowColorChanged));

        public static readonly DependencyProperty InactiveGlowBrushProperty = DependencyProperty.Register(
                "InactiveGlowBrush", typeof(SolidColorBrush), typeof(VisualStudioGlowWindowBehavior),
                new PropertyMetadata(Brushes.Transparent, OnGlowColorChanged));


        public SolidColorBrush ActiveGlowBrush
        {
            get => (SolidColorBrush)GetValue(ActiveGlowBrushProperty);
            set => SetValue(ActiveGlowBrushProperty, value);
        }

        public SolidColorBrush InactiveGlowBrush
        {
            get => (SolidColorBrush)GetValue(InactiveGlowBrushProperty);
            set => SetValue(InactiveGlowBrushProperty, value);
        }

        private static void OnGlowColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is VisualStudioGlowWindowBehavior glowWindowBehavior)
            {
                glowWindowBehavior.UpdateGlowColors();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            var window = AssociatedObject;
            window.Activated += Window_Activated;
            window.Deactivated += Window_Deactivated;
            window.SourceInitialized += Window_SourceInitialized;
            window.Closed += Window_Closed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var window = AssociatedObject;
            window.Activated -= Window_Activated;
            window.Deactivated -= Window_Deactivated;
            window.SourceInitialized -= Window_SourceInitialized;
            window.Closed -= Window_Closed;
        }


        private void Window_Activated(object? sender, EventArgs e)
        {
            UpdateGlowActiveState();
        }
        private void Window_Deactivated(object? sender, EventArgs e)
        {
            UpdateGlowActiveState();
        }
        private void Window_SourceInitialized(object? sender, EventArgs e)
        {
            var hwNdSource = GetHwNdSource(AssociatedObject);
            if (hwNdSource is not null)
            {
                hwNdSource.AddHook(HwndSourceHook);
                CreateGlowWindowHandles();
            }
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            StopTimer();
            DestroyGlowWindows();
        }

        private void UpdateGlowColors()
        {
            using (DeferGlowChanges())
            {
                foreach (var current in LoadedGlowWindows)
                {
                    current.ActiveGlowBrush = ActiveGlowBrush;
                    current.InactiveGlowBrush = InactiveGlowBrush;
                }
            }
        }


        #region GlowWindow

        private readonly GlowEdge?[] _glowEdges = new GlowEdge[4];
        private bool _isGlowVisible;
        private Rect _logicalSizeForRestore = Rect.Empty;
        private DispatcherTimer? _makeGlowVisibleTimer;
        private bool _updatingZOrder;
        private bool _useLogicalSizeForRestore;
        internal int DeferGlowChangesCount;

        private IEnumerable<GlowEdge> LoadedGlowWindows => from w in _glowEdges where w != null select w;

        private bool IsGlowVisible
        {
            get => _isGlowVisible;
            set
            {
                if (_isGlowVisible != value)
                {
                    _isGlowVisible = value;
                    for (var i = 0; i < _glowEdges.Length; i++) GetOrCreateGlowWindow(i).IsVisible = value;
                }
            }
        }

        protected bool ShouldShowGlow
        {
            get
            {
                var handle = this.GetHandle();
                return InteropMethods.IsWindowVisible(handle) && !InteropMethods.IsIconic(handle) &&
                       !InteropMethods.IsZoomed(handle) && AssociatedObject.ResizeMode != ResizeMode.NoResize;
            }
        }

        internal void EndDeferGlowChanges()
        {
            foreach (var current in LoadedGlowWindows) current.CommitChanges();
        }

        private IDisposable DeferGlowChanges()
        {
            return new ChangeScope(this);
        }

        public static IntPtr GetHandle(Window window)
        {
            return new WindowInteropHelper(window).EnsureHandle();
        }

        public static HwndSource GetHwNdSource(Window window)
        {
            return HwndSource.FromHwnd(GetHandle(window));
        }

        private IntPtr HwndSourceHook(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg <= InteropValues.WmWindowPosChanged)
            {
                if (msg == InteropValues.WmActivate) return IntPtr.Zero;

                if (msg != InteropValues.WmQuit)
                    switch (msg)
                    {
                        case InteropValues.WmWindowPosChanging:
                            WmWindowPosChanging(lParam);
                            return IntPtr.Zero;
                        case InteropValues.WmWindowPosChanged:
                            WmWindowPosChanged(lParam);
                            return IntPtr.Zero;
                        default:
                            return IntPtr.Zero;
                    }
            }
            else
            {
                if (msg <= InteropValues.WmNcRButtonDblClk)
                    switch (msg)
                    {
                        case InteropValues.WmSetIcon:
                            break;
                        case InteropValues.WmNcCreate:
                        case InteropValues.WmNcDestroy:
                            return IntPtr.Zero;
                        case InteropValues.WmNcActivate:
                            handled = true;
                            return WmNcActivate(hWnd, wParam);
                        default:
                            switch (msg)
                            {
                                case InteropValues.WmNcRButtonDown:
                                case InteropValues.WmNcRButtonUp:
                                case InteropValues.WmNcRButtonDblClk:
                                    handled = true;
                                    return IntPtr.Zero;
                                default:
                                    return IntPtr.Zero;
                            }
                    }
                else
                    switch (msg)
                    {
                        case InteropValues.WmNcUahDrawCaption:
                        case InteropValues.WmNcUahDrawFrame:
                            handled = true;
                            return IntPtr.Zero;
                        default:
                            if (msg != InteropValues.WmSysCommand) return IntPtr.Zero;

                            WmSysCommand(hWnd, wParam);
                            return IntPtr.Zero;
                    }
            }

            handled = true;
            return CallDefWindowProcWithoutVisibleStyle(hWnd, msg, wParam, lParam);
        }

        private GlowEdge GetOrCreateGlowWindow(int direction)
        {
            return _glowEdges[direction] ?? (_glowEdges[direction] = new GlowEdge(this, (Dock)direction)
            {
                ActiveGlowBrush = ActiveGlowBrush,
                InactiveGlowBrush = InactiveGlowBrush,
                IsActive = AssociatedObject.IsActive
            });
        }

        public bool DoNotUseTimerTick { get; set; }

        private void UpdateGlowVisibility(bool delayIfNecessary)
        {
            var shouldShowGlow = ShouldShowGlow;
            if (shouldShowGlow != IsGlowVisible)
            {
                if (DoNotUseTimerTick)
                {
                    IsGlowVisible = shouldShowGlow;
                    return;
                }
                if (SystemParameters.MinimizeAnimation && shouldShowGlow && delayIfNecessary)
                {
                    if (_makeGlowVisibleTimer != null)
                    {
                        _makeGlowVisibleTimer.Stop();
                    }
                    else
                    {
                        _makeGlowVisibleTimer = new DispatcherTimer
                        {
                            Interval = TimeSpan.FromMilliseconds(200.0)
                        };
                        _makeGlowVisibleTimer.Tick += OnDelayedVisibilityTimerTick;
                    }

                    _makeGlowVisibleTimer.Start();
                    return;
                }

                StopTimer();
                IsGlowVisible = shouldShowGlow;
            }
        }

        private void StopTimer()
        {
            if (_makeGlowVisibleTimer != null)
            {
                _makeGlowVisibleTimer.Stop();
                _makeGlowVisibleTimer.Tick -= OnDelayedVisibilityTimerTick;
                _makeGlowVisibleTimer = null;
            }
        }

        private void OnDelayedVisibilityTimerTick(object? sender, EventArgs e)
        {
            StopTimer();
            UpdateGlowWindowPositions(false);
        }

        private void UpdateGlowWindowPositions(bool delayIfNecessary)
        {
            using (DeferGlowChanges())
            {
                UpdateGlowVisibility(delayIfNecessary);
                foreach (var current in LoadedGlowWindows) current.UpdateWindowPos();
            }
        }

        private void UpdateGlowActiveState()
        {
            using (DeferGlowChanges())
            {
                foreach (var current in LoadedGlowWindows) current.IsActive = AssociatedObject.IsActive;
            }
        }

        private void DestroyGlowWindows()
        {
            for (var i = 0; i < _glowEdges.Length; i++)
                using (_glowEdges[i])
                {
                    _glowEdges[i] = null;
                }
        }

        private void WmWindowPosChanging(IntPtr lParam)
        {
            var windowPos = (InteropValues.WindowPos?)Marshal.PtrToStructure(lParam, typeof(InteropValues.WindowPos));
            if (windowPos is not null && (windowPos.flags & 2u) == 0u && (windowPos.flags & 1u) == 0u && windowPos.cx > 0 &&
                windowPos.cy > 0)
            {
                var rect = new Rect(windowPos.x, windowPos.y, windowPos.cx, windowPos.cy);
                rect = rect.DeviceToLogicalUnits();
                if (_useLogicalSizeForRestore)
                {
                    rect = _logicalSizeForRestore;
                    _logicalSizeForRestore = Rect.Empty;
                    _useLogicalSizeForRestore = false;
                }

                var logicalRect = GetOnScreenPosition(rect);
                logicalRect = logicalRect.LogicalToDeviceUnits();
                windowPos.x = (int)logicalRect.X;
                windowPos.y = (int)logicalRect.Y;
                Marshal.StructureToPtr(windowPos, lParam, true);
            }
        }

        private static void UpdateZOrderOfOwner(IntPtr hwndOwner)
        {
            var lastOwnedWindow = IntPtr.Zero;
            InteropMethods.EnumThreadWindows(InteropMethods.GetCurrentThreadId(), delegate (IntPtr hwNd, IntPtr _)
            {
                if (InteropMethods.GetWindow(hwNd, 4) == hwndOwner) lastOwnedWindow = hwNd;

                return true;
            }, IntPtr.Zero);

            if (lastOwnedWindow != IntPtr.Zero && InteropMethods.GetWindow(hwndOwner, 3) != lastOwnedWindow)
                InteropMethods.SetWindowPos(hwndOwner, lastOwnedWindow, 0, 0, 0, 0, 19);
        }

        private void UpdateZOrderOfThisAndOwner()
        {
            if (_updatingZOrder) return;

            try
            {
                _updatingZOrder = true;
                var windowInteropHelper = new WindowInteropHelper(AssociatedObject);
                var handle = windowInteropHelper.Handle;
                foreach (var current in LoadedGlowWindows)
                {
                    var window = InteropMethods.GetWindow(current.Handle, 3);
                    if (window != handle) InteropMethods.SetWindowPos(current.Handle, handle, 0, 0, 0, 0, 19);

                    handle = current.Handle;
                }

                var owner = windowInteropHelper.Owner;
                if (owner != IntPtr.Zero) UpdateZOrderOfOwner(owner);
            }
            finally
            {
                _updatingZOrder = false;
            }
        }

        private void WmWindowPosChanged(IntPtr lParam)
        {
            try
            {
                var windowPos = (InteropValues.WindowPos?)Marshal.PtrToStructure(lParam, typeof(InteropValues.WindowPos));

                if (windowPos is not null)
                {
                    UpdateGlowWindowPositions((windowPos.flags & 64u) == 0u);
                    UpdateZOrderOfThisAndOwner();
                }
            }
            catch
            {
                // ignored
            }
        }

        private static Rect GetOnScreenPosition(Rect floatRect)
        {
            var result = floatRect;
            floatRect = floatRect.LogicalToDeviceUnits();
            ScreenHelper.FindMaximumSingleMonitorRectangle(floatRect, out _, out var rect2);
            if (!floatRect.IntersectsWith(rect2))
            {
                ScreenHelper.FindMonitorRectsFromPoint(InteropMethods.GetCursorPos(), out _, out rect2);
                rect2 = rect2.DeviceToLogicalUnits();
                if (result.Width > rect2.Width) result.Width = rect2.Width;

                if (result.Height > rect2.Height) result.Height = rect2.Height;

                if (rect2.Right <= result.X) result.X = rect2.Right - result.Width;

                if (rect2.Left > result.X + result.Width) result.X = rect2.Left;

                if (rect2.Bottom <= result.Y) result.Y = rect2.Bottom - result.Height;

                if (rect2.Top > result.Y + result.Height) result.Y = rect2.Top;
            }

            return result;
        }

        private static InteropValues.MonitorInfo MonitorInfoFromWindow(IntPtr hWnd)
        {
            var hMonitor = InteropMethods.MonitorFromWindow(hWnd, 2);
            var result = default(InteropValues.MonitorInfo);
            result.CbSize = (uint)Marshal.SizeOf(typeof(InteropValues.MonitorInfo));
            InteropMethods.GetMonitorInfo(hMonitor, ref result);
            return result;
        }

        private static IntPtr WmNcActivate(IntPtr hWnd, IntPtr wParam)
        {
            return InteropMethods.DefWindowProc(hWnd, InteropValues.WmNcActivate, wParam, InteropMethods.HrGnNone);
        }

        private bool IsAeroSnappedToMonitor(IntPtr hWnd)
        {
            var monitorinfo = MonitorInfoFromWindow(hWnd);
            var logicalRect = new Rect(AssociatedObject.Left, AssociatedObject.Top, AssociatedObject.Width, AssociatedObject.Height);
            logicalRect = logicalRect.LogicalToDeviceUnits();
            return MathHelper.AreClose(monitorinfo.RcWork.Height, logicalRect.Height) &&
                   MathHelper.AreClose(monitorinfo.RcWork.Top, logicalRect.Top);
        }

        private void WmSysCommand(IntPtr hWnd, IntPtr wParam)
        {
            var num = InteropMethods.GET_SC_WPARAM(wParam);

            if (num == InteropValues.ScMove)
                InteropMethods.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero,
                    InteropValues.RedrawWindowFlags.Invalidate | InteropValues.RedrawWindowFlags.NoChildren |
                    InteropValues.RedrawWindowFlags.UpdateNow | InteropValues.RedrawWindowFlags.Frame);

            if ((num == InteropValues.ScMaximize || num == InteropValues.ScMinimize || num == InteropValues.ScMove ||
                 num == InteropValues.ScSize) && AssociatedObject.WindowState == WindowState.Normal && !IsAeroSnappedToMonitor(hWnd))
                _logicalSizeForRestore = new Rect(AssociatedObject.Left, AssociatedObject.Top, AssociatedObject.Width, AssociatedObject.Height);

            if (num == InteropValues.ScMove && AssociatedObject.WindowState == WindowState.Maximized && _logicalSizeForRestore == Rect.Empty)
                _logicalSizeForRestore = new Rect(AssociatedObject.Left, AssociatedObject.Top, AssociatedObject.Width, AssociatedObject.Height);

            if (num == InteropValues.ScRestore && AssociatedObject.WindowState != WindowState.Minimized &&
                _logicalSizeForRestore.Width > 0.0 && _logicalSizeForRestore.Height > 0.0)
            {
                AssociatedObject.Left = _logicalSizeForRestore.Left;
                AssociatedObject.Top = _logicalSizeForRestore.Top;
                AssociatedObject.Width = _logicalSizeForRestore.Width;
                AssociatedObject.Height = _logicalSizeForRestore.Height;
                _useLogicalSizeForRestore = true;
            }
        }

        private static IntPtr CallDefWindowProcWithoutVisibleStyle(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            var flag = VisualHelper.ModifyStyle(hWnd, InteropValues.WsVisible, 0);
            var result = InteropMethods.DefWindowProc(hWnd, msg, wParam, lParam);
            if (flag) VisualHelper.ModifyStyle(hWnd, 0, InteropValues.WsVisible);

            return result;
        }

        private void CreateGlowWindowHandles()
        {
            for (var i = 0; i < _glowEdges.Length; i++) GetOrCreateGlowWindow(i).EnsureHandle();
        }
        #endregion

        protected internal virtual IntPtr GetHandle()
        {
            return AssociatedObject.GetHandle();
        }

        protected internal virtual WindowInteropHelper GetWindowInteropHelper()
        {
            return new WindowInteropHelper(AssociatedObject);
        }
    }
}
