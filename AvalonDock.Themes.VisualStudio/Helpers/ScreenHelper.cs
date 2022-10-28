using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace AvalonDock.Themes.VisualStudio.Helpers
{
    internal class ScreenHelper
    {
        internal static void FindMaximumSingleMonitorRectangle(Rect windowRect, out Rect screenSubRect,
            out Rect monitorRect)
        {
            var windowRect2 = new InteropValues.Rect(windowRect);
            FindMaximumSingleMonitorRectangle(windowRect2, out var rect, out var rect2);
            screenSubRect = new Rect(rect.Position, rect.RSize);
            monitorRect = new Rect(rect2.Position, rect2.RSize);
        }

        private static void FindMaximumSingleMonitorRectangle(InteropValues.Rect windowRect,
            out InteropValues.Rect screenSubRect, out InteropValues.Rect monitorRect)
        {
            var rects = new List<InteropValues.Rect>();
            InteropMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                delegate (IntPtr hMonitor, IntPtr _, ref InteropValues.Rect _, IntPtr _)
                {
                    var monitorInfo = default(InteropValues.MonitorInfo);
                    monitorInfo.CbSize = (uint)Marshal.SizeOf(typeof(InteropValues.MonitorInfo));
                    InteropMethods.GetMonitorInfo(hMonitor, ref monitorInfo);
                    rects.Add(monitorInfo.RcWork);
                    return true;
                }, IntPtr.Zero);

            var num = 0L;

            screenSubRect = new InteropValues.Rect
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };

            monitorRect = new InteropValues.Rect
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };

            foreach (var current in rects)
            {
                var rect = current;
                InteropMethods.IntersectRect(out var rEct2, ref rect, ref windowRect);
                var num2 = (long)(rEct2.Width * rEct2.Height);
                if (num2 > num)
                {
                    screenSubRect = rEct2;
                    monitorRect = current;
                    num = num2;
                }
            }
        }

        internal static void FindMonitorRectsFromPoint(Point point, out Rect monitorRect, out Rect workAreaRect)
        {
            var intPtr = InteropMethods.MonitorFromPoint(new InteropValues.Point
            {
                X = (int)point.X,
                Y = (int)point.Y
            }, 2);

            monitorRect = new Rect(0.0, 0.0, 0.0, 0.0);
            workAreaRect = new Rect(0.0, 0.0, 0.0, 0.0);

            if (intPtr != IntPtr.Zero)
            {
                InteropValues.MonitorInfo monitorInfo = default;
                monitorInfo.CbSize = (uint)Marshal.SizeOf(typeof(InteropValues.MonitorInfo));
                InteropMethods.GetMonitorInfo(intPtr, ref monitorInfo);
                monitorRect = new Rect(monitorInfo.RcMonitor.Position, monitorInfo.RcMonitor.RSize);
                workAreaRect = new Rect(monitorInfo.RcWork.Position, monitorInfo.RcWork.RSize);
            }
        }
    }
}