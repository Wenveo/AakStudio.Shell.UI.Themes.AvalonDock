using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;
using System.Windows;
using System.Windows.Media;

namespace AvalonDock.Themes.VisualStudio.Helpers
{
    internal static class DpiHelper
    {
        private const double LOGICAL_DPI = 96.0;

        static DpiHelper()
        {
            var dC = InteropMethods.GetDC(IntPtr.Zero);
            if (dC != IntPtr.Zero)
            {
                // 沿着屏幕宽度每逻辑英寸的像素数。在具有多个显示器的系统中，这个值对所有显示器都是相同的
                const int logicPixelsX = 88;
                // 沿着屏幕高度每逻辑英寸的像素数
                const int logicPixelsY = 90;
                DeviceDpiX = InteropMethods.GetDeviceCaps(dC, logicPixelsX);
                DeviceDpiY = InteropMethods.GetDeviceCaps(dC, logicPixelsY);
                _ = InteropMethods.ReleaseDC(IntPtr.Zero, dC);
            }
            else
            {
                DeviceDpiX = LOGICAL_DPI;
                DeviceDpiY = LOGICAL_DPI;
            }

            var identity = Matrix.Identity;
            var identity2 = Matrix.Identity;
            identity.Scale(DeviceDpiX / LOGICAL_DPI, DeviceDpiY / LOGICAL_DPI);
            identity2.Scale(LOGICAL_DPI / DeviceDpiX, LOGICAL_DPI / DeviceDpiY);
            TransformFromDevice = new MatrixTransform(identity2);
            TransformFromDevice.Freeze();
            TransformToDevice = new MatrixTransform(identity);
            TransformToDevice.Freeze();
        }

        public static MatrixTransform TransformFromDevice { get; }

        public static MatrixTransform TransformToDevice { get; }

        public static double DeviceDpiX { get; }

        public static double DeviceDpiY { get; }

        public static Rect LogicalToDeviceUnits(this Rect logicalRect)
        {
            var result = logicalRect;
            result.Transform(TransformToDevice.Matrix);
            return result;
        }

        public static Rect DeviceToLogicalUnits(this Rect deviceRect)
        {
            var result = deviceRect;
            result.Transform(TransformFromDevice.Matrix);
            return result;
        }
    }
}