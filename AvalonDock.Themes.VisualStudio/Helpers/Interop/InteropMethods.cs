using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

namespace AvalonDock.Themes.VisualStudio.Helpers.Interop
{
    internal class InteropMethods
    {
        #region common
        internal const int EFail = unchecked((int)0x80004005);

        internal static readonly IntPtr HrGnNone = new(-1);

        [DllImport(InteropValues.ExternDll.User32)]
        internal static extern int GetWindowRect(IntPtr hwNd, out InteropValues.Rect lpRect);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto)]
        internal static extern bool GetCursorPos(out InteropValues.Point pt);

        [SecurityCritical]
        [SuppressUnmanagedCodeSecurity]
        [DllImport(InteropValues.ExternDll.Gdi32, EntryPoint = nameof(DeleteObject), CharSet = CharSet.Auto,
            SetLastError = true)]
        private static extern bool IntDeleteObject(IntPtr hObject);

        [SecurityCritical]
        internal static bool DeleteObject(IntPtr hObject)
        {
            var result = IntDeleteObject(hObject);
            return result;
        }

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto)]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport(InteropValues.ExternDll.Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string? lpModuleName);

        [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
        internal static extern int ReleaseDC(IntPtr window, IntPtr dc);

        [DllImport(InteropValues.ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDC(IntPtr ptr);

        [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hwNd, InteropValues.WindowPlacement lpWnDpl);

        internal static InteropValues.WindowPlacement GetWindowPlacement(IntPtr hwNd)
        {
            var windowPlacement = new InteropValues.WindowPlacement();
            if (GetWindowPlacement(hwNd, windowPlacement)) return windowPlacement;
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        internal static int GetXlParam(int value)
        {
            return (short)(value & 65535);
        }

        internal static int GetYlParam(int lParam)
        {
            return (short)(lParam >> 16);
        }
        public static Point GetMousePosition()
        {
            GetCursorPos(out var pos);
            return new Point(pos.X, pos.Y);
        }

        [DllImport(InteropValues.ExternDll.User32)]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumThreadWindows(uint dwThreadId, InteropValues.EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport(InteropValues.ExternDll.Gdi32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport(InteropValues.ExternDll.Gdi32, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport(InteropValues.ExternDll.Gdi32, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(InteropValues.ExternDll.User32)]
        internal static extern IntPtr MonitorFromPoint(InteropValues.Point pt, int flags);

        [DllImport(InteropValues.ExternDll.User32)]
        internal static extern IntPtr GetWindow(IntPtr hwnd, int nCmd);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsIconic(IntPtr hwnd);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsZoomed(IntPtr hwnd);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            int flags);

        internal static Point GetCursorPos()
        {
            var result = default(Point);
            if (GetCursorPos(out var point))
            {
                result.X = point.X;
                result.Y = point.Y;
            }

            return result;
        }

        [DllImport(InteropValues.ExternDll.User32)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        internal static int GetWindowLong(IntPtr hWnd, InteropValues.Gwl nIndex)
        {
            return GetWindowLong(hWnd, (int)nIndex);
        }

        internal static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4) return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        internal static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Unicode)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        internal static IntPtr SetWindowLongPtr(IntPtr hWnd, InteropValues.GwLp nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) return SetWindowLongPtr(hWnd, (int)nIndex, dwNewLong);
            return new IntPtr(SetWindowLong(hWnd, (int)nIndex, dwNewLong.ToInt32()));
        }

        internal static int SetWindowLong(IntPtr hWnd, InteropValues.Gwl nIndex, int dwNewLong)
        {
            return SetWindowLong(hWnd, (int)nIndex, dwNewLong);
        }

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Unicode)]
        internal static extern ushort RegisterClass(ref InteropValues.WndClass lpWndClass);

        [DllImport(InteropValues.ExternDll.Kernel32)]
        internal static extern uint GetCurrentThreadId();

        [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle,
            int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDest, ref InteropValues.Point pptDest,
            ref InteropValues.Size psize, IntPtr hdcSrc, ref InteropValues.Point pptSrc, uint crKey,
            [In] ref InteropValues.BlendFunction pblend, uint dwFlags);

        [DllImport(InteropValues.ExternDll.User32)]
        internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate,
            InteropValues.RedrawWindowFlags flags);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
            InteropValues.EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IntersectRect(out InteropValues.Rect lprcDst, [In] ref InteropValues.Rect lprcSrc1,
            [In] ref InteropValues.Rect lprcSrc2);

        [DllImport(InteropValues.ExternDll.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref InteropValues.MonitorInfo monitorInfo);

        [DllImport(InteropValues.ExternDll.Gdi32, SetLastError = true)]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc, ref InteropValues.BitmapInfo pbmi, uint iUsage,
            out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport(InteropValues.ExternDll.MsImg)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest,
            IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, InteropValues.BlendFunction pfn);

        internal static int GET_SC_WPARAM(IntPtr wParam)
        {
            return (int)wParam & 65520;
        }

        #endregion
    }
}