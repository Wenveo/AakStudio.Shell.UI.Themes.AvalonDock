using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace AvalonDock.Themes.VisualStudio.Helpers
{
    internal static class VisualHelper
    {
        public static T? GetChild<T>(DependencyObject d) where T : DependencyObject
        {
            if (d is null) return default;

            if (d is T t) return t;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);

                var result = GetChild<T>(child);
                if (result != null) return result;
            }

            return default;
        }

        public static T? GetParent<T>(DependencyObject d) where T : DependencyObject
        {
            if (d is null)
                return default;
            if (d is T t)
                return t;
            if (d is Window)
                return null;
            return GetParent<T>(VisualTreeHelper.GetParent(d));
        }

        public static IntPtr GetHandle(this Visual visual)
        {
            return (PresentationSource.FromVisual(visual) as HwndSource)?.Handle ?? IntPtr.Zero;
        }

        internal static bool ModifyStyle(IntPtr hWnd, int styleToRemove, int styleToAdd)
        {
            var windowLong = InteropMethods.GetWindowLong(hWnd, InteropValues.Gwl.Style);
            var num = (windowLong & ~styleToRemove) | styleToAdd;
            if (num == windowLong) return false;

            InteropMethods.SetWindowLong(hWnd, InteropValues.Gwl.Style, num);
            return true;
        }
    }
}