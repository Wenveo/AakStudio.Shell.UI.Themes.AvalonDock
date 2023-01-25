using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace AakStudio.Shell.UI.Themes.AvalonDock.Controls
{
    internal static class MouseHelper
    {
        [DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern unsafe IntPtr GetCursorPos(global::System.Drawing.Point* lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct PointWrap
        {
            public int X;
            public int Y;
        }

        private static bool GetPointWrap(out PointWrap point)
        {
            unsafe
            {
                fixed (PointWrap* ptr = &point)
                {
                    return GetCursorPos((System.Drawing.Point*)ptr) != IntPtr.Zero;
                }
            }
        }

        public static Point GetMousePosition()
        {
            if (GetPointWrap(out var point))
            {
                return new Point(point.X, point.Y);
            }

            return new Point();
        }
    }
}
