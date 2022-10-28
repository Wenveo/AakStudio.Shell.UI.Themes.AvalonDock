using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AvalonDock.Themes.VisualStudio.Controls.GlowWindow
{
    internal class GlowBitmap : DisposableObject
    {
        internal const int GlowBitmapPartCount = 16;

        private const int BYTES_PER_PIXEL_RGBA32 = 4;

        private static readonly CachedBitmapInfo[] TransparencyMasks = new CachedBitmapInfo[GlowBitmapPartCount];

        private readonly InteropValues.BitmapInfo _bitmapInfo;

        private readonly IntPtr _pBits;

        internal GlowBitmap(IntPtr hdcScreen, int width, int height)
        {
            _bitmapInfo.biSize = 40; /*Marshal.SizeOf(typeof(InteropValues.Bitmapinfoheader));*/
            _bitmapInfo.biPlanes = 1;
            _bitmapInfo.biBitCount = 32;
            _bitmapInfo.biCompression = 0;
            _bitmapInfo.biXPeLsPerMeter = 0;
            _bitmapInfo.biYPeLsPerMeter = 0;
            _bitmapInfo.biWidth = width;
            _bitmapInfo.biHeight = -height;

            Handle = InteropMethods.CreateDIBSection(
                hdcScreen,
                ref _bitmapInfo,
                0u,
                out _pBits,
                IntPtr.Zero,
                0u);
        }

        internal IntPtr Handle { get; }

        internal IntPtr DiBits => _pBits;

        internal int Width => _bitmapInfo.biWidth;

        internal int Height => -_bitmapInfo.biHeight;

        protected override void DisposeNativeResources()
        {
            InteropMethods.DeleteObject(Handle);
        }

        private static byte PreMultiplyAlpha(byte channel, byte alpha)
        {
            return (byte)(channel * alpha / 255.0);
        }

        internal static GlowBitmap? Create(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart, Color color)
        {
            var orCreateAlphaMask =
                GetOrCreateAlphaMask(bitmapPart);

            if (orCreateAlphaMask is null)
            {
                return default;
            }
            var glowBitmap =
                new GlowBitmap(
                    drawingContext.ScreenDc,
                    orCreateAlphaMask.Width,
                    orCreateAlphaMask.Height);

            for (var i = 0; i < orCreateAlphaMask.DiBits.Length; i += BYTES_PER_PIXEL_RGBA32)
            {
                var b = orCreateAlphaMask.DiBits[i + 3];
                var val = PreMultiplyAlpha(color.R, b);
                var val2 = PreMultiplyAlpha(color.G, b);
                var val3 = PreMultiplyAlpha(color.B, b);
                Marshal.WriteByte(glowBitmap.DiBits, i, val3);
                Marshal.WriteByte(glowBitmap.DiBits, i + 1, val2);
                Marshal.WriteByte(glowBitmap.DiBits, i + 2, val);
                Marshal.WriteByte(glowBitmap.DiBits, i + 3, b);
            }

            return glowBitmap;
        }

        private static CachedBitmapInfo? GetOrCreateAlphaMask(GlowBitmapPart bitmapPart)
        {
            if (TransparencyMasks is null)
            {
                return default;
            }
            if (TransparencyMasks[(int)bitmapPart] == null)
            {
                var bitmapImage =
                    new BitmapImage(new Uri(
                        $@"pack://application:,,,/AvalonDock.Themes.VisualStudio;Component/Resources/Images/GlowWindow/{bitmapPart}.png"));

                var array = new byte[BYTES_PER_PIXEL_RGBA32 * bitmapImage.PixelWidth * bitmapImage.PixelHeight];
                var stride = BYTES_PER_PIXEL_RGBA32 * bitmapImage.PixelWidth;
                bitmapImage.CopyPixels(array, stride, 0);
                bitmapImage.Freeze();

                TransparencyMasks[(int)bitmapPart] =
                    new CachedBitmapInfo(
                        array,
                        bitmapImage.PixelWidth,
                        bitmapImage.PixelHeight);
            }

            return TransparencyMasks[(int)bitmapPart];
        }

        private sealed class CachedBitmapInfo
        {
            internal readonly byte[] DiBits;
            internal readonly int Height;
            internal readonly int Width;

            internal CachedBitmapInfo(byte[] diBits, int width, int height)
            {
                Width = width;
                Height = height;
                DiBits = diBits;
            }
        }
    }
}