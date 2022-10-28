using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;

namespace AvalonDock.Themes.VisualStudio.Controls.GlowWindow
{
    internal class GlowDrawingContext : DisposableObject
    {
        private readonly GlowBitmap? _windowBitmap;

        internal InteropValues.BlendFunction Blend;

        internal GlowDrawingContext(int width, int height)
        {
            ScreenDc = InteropMethods.GetDC(IntPtr.Zero);
            if (ScreenDc == IntPtr.Zero) return;

            WindowDc = InteropMethods.CreateCompatibleDC(ScreenDc);
            if (WindowDc == IntPtr.Zero) return;

            BackgroundDc = InteropMethods.CreateCompatibleDC(ScreenDc);
            if (BackgroundDc == IntPtr.Zero) return;

            Blend.BlendOp = 0;
            Blend.BlendFlags = 0;
            Blend.SourceConstantAlpha = 255;
            Blend.AlphaFormat = 1;
            _windowBitmap = new GlowBitmap(ScreenDc, width, height);
            InteropMethods.SelectObject(WindowDc, _windowBitmap.Handle);
        }

        internal bool IsInitialized =>
            ScreenDc != IntPtr.Zero && WindowDc != IntPtr.Zero &&
            BackgroundDc != IntPtr.Zero && _windowBitmap != null;

        internal IntPtr ScreenDc { get; }

        internal IntPtr WindowDc { get; }

        internal IntPtr BackgroundDc { get; }

        internal int Width => _windowBitmap?.Width ?? 0;

        internal int Height => _windowBitmap?.Height ?? 0;

        protected override void DisposeManagedResources()
        {
            _windowBitmap?.Dispose();
        }

        protected override void DisposeNativeResources()
        {
            if (ScreenDc != IntPtr.Zero) _ = InteropMethods.ReleaseDC(IntPtr.Zero, ScreenDc);

            if (WindowDc != IntPtr.Zero) InteropMethods.DeleteDC(WindowDc);

            if (BackgroundDc != IntPtr.Zero) InteropMethods.DeleteDC(BackgroundDc);
        }
    }
}