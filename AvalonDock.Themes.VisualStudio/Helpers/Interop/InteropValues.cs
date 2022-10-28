using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace AvalonDock.Themes.VisualStudio.Helpers.Interop
{
    internal class InteropValues
    {
        internal static class ExternDll
        {
            public const string
                User32 = "user32.dll",
                Gdi32 = "gdi32.dll",
                Kernel32 = "kernel32.dll",
                MsImg = "msimg32.dll";
        }

        internal delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        internal const int
            EFail = unchecked((int)0x80004005),
            WmActivate = 0x0006,
            WmQuit = 0x0012,
            WmWindowPosChanging = 0x0046,
            WmWindowPosChanged = 0x0047,
            WmSetIcon = 0x0080,
            WmNcCreate = 0x0081,
            WmNcDestroy = 0x0082,
            WmNcActivate = 0x0086,
            WmNcRButtonDown = 0x00A4,
            WmNcRButtonUp = 0x00A5,
            WmNcRButtonDblClk = 0x00A6,
            WmNcUahDrawCaption = 0x00AE,
            WmNcUahDrawFrame = 0x00AF,
            WmSysCommand = 0x112,
            WsVisible = 0x10000000,
            ScSize = 0xF000,
            ScMove = 0xF010,
            ScMinimize = 0xF020,
            ScMaximize = 0xF030,
            ScRestore = 0xF120,
            NCLBUTTONDOWN = 161,
            CAPTION = 2;

        [StructLayout(LayoutKind.Sequential)]
        internal struct Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public Rect(System.Windows.Rect rect)
            {
                Left = (int)rect.Left;
                Top = (int)rect.Top;
                Right = (int)rect.Right;
                Bottom = (int)rect.Bottom;
            }

            public System.Windows.Point Position => new(Left, Top);
            public System.Windows.Size RSize => new(Width, Height);

            public int Height
            {
                get => Bottom - Top;
                set => Bottom = Top + value;
            }

            public int Width
            {
                get => Right - Left;
                set => Right = Left + value;
            }
        }

        internal struct BlendFunction
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        internal enum Gwl
        {
            Style = -16,
        }

        internal enum GwLp
        {
            HwNdParent = -8,
            Id = -12
        }

        [Flags]
        internal enum RedrawWindowFlags : uint
        {
            Invalidate = 1u,
            NoChildren = 64u,
            UpdateNow = 256u,
            Frame = 1024u,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class WindowPos
        {
            public IntPtr HwNd;
            public IntPtr HwNdInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class WindowPlacement
        {
            public int length = Marshal.SizeOf(typeof(WindowPlacement));
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rect rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct Size
        {
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LONG")]
            public int cx;
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LONG")]
            public int cy;
        }

        internal struct MonitorInfo
        {
            public uint CbSize;
            public Rect RcMonitor;
            public Rect RcWork;
            public uint DwFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct WndClass
        {
            public uint style;
            public Delegate LpFnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string? lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string? lpszClassName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal struct BitmapInfo
        {
            public int biSize;

            public int biWidth;

            public int biHeight;

            public short biPlanes;

            public short biBitCount;

            public int biCompression;

            public int biSizeImage;

            public int biXPeLsPerMeter;

            public int biYPeLsPerMeter;

            public int biClrUsed;

            public int biClrImportant;

            public BitmapInfo(int width, int height, short bpp)
            {
                biSize = SizeOf();
                biWidth = width;
                biHeight = height;
                biPlanes = 1;
                biBitCount = bpp;
                biCompression = 0;
                biSizeImage = 0;
                biXPeLsPerMeter = 0;
                biYPeLsPerMeter = 0;
                biClrUsed = 0;
                biClrImportant = 0;
            }

            [SecuritySafeCritical]
            private static int SizeOf()
            {
                return Marshal.SizeOf(typeof(BitmapInfo));
            }
        }
        [ComImport, Guid("0000000C-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IStream
        {
            int Read([In] IntPtr buf, [In] int len);

            int Write([In] IntPtr buf, [In] int len);

            [return: MarshalAs(UnmanagedType.I8)]
            long Seek([In, MarshalAs(UnmanagedType.I8)] long dLibMove, [In] int dwOrigin);

            void SetSize([In, MarshalAs(UnmanagedType.I8)] long libNewSize);

            [return: MarshalAs(UnmanagedType.I8)]
            long CopyTo([In, MarshalAs(UnmanagedType.Interface)] IStream psTm, [In, MarshalAs(UnmanagedType.I8)] long cb, [Out, MarshalAs(UnmanagedType.LPArray)] long[] pcbRead);

            void Commit([In] int grfCommitFlags);

            void Revert();

            void LockRegion([In, MarshalAs(UnmanagedType.I8)] long libOffset, [In, MarshalAs(UnmanagedType.I8)] long cb, [In] int dwLockType);

            void UnlockRegion([In, MarshalAs(UnmanagedType.I8)] long libOffset, [In, MarshalAs(UnmanagedType.I8)] long cb, [In] int dwLockType);

            void Stat([In] IntPtr pStatsTg, [In] int grfStatFlag);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStream? Clone();
        }

        internal class StreamConst
        {
            public const int StreamSeekSet = 0x0;
            public const int StreamSeekCur = 0x1;
            public const int StreamSeekEnd = 0x2;
        }

        internal class ComStreamFromDataStream : IStream
        {
            protected Stream DataStream;

            // to support seeking ahead of the stream length...
            private long _virtualPosition = -1;

            internal ComStreamFromDataStream(Stream dataStream)
            {
                this.DataStream = dataStream ?? throw new ArgumentNullException(nameof(dataStream));
            }

            private void ActualizeVirtualPosition()
            {
                if (_virtualPosition == -1)
                {
                    return;
                }

                if (_virtualPosition > DataStream.Length)
                {
                    DataStream.SetLength(_virtualPosition);
                }

                DataStream.Position = _virtualPosition;

                _virtualPosition = -1;
            }

            public virtual IStream? Clone()
            {
                NotImplemented();
                return null;
            }

            public virtual void Commit(int grfCommitFlags)
            {
                DataStream.Flush();
                ActualizeVirtualPosition();
            }

            public virtual long CopyTo(IStream psTm, long cb, long[] pcbRead)
            {
                const int bufSize = 4096; // one page
                var buffer = Marshal.AllocHGlobal(bufSize);
                if (buffer == IntPtr.Zero)
                {
                    throw new OutOfMemoryException();
                }

                long written = 0;

                try
                {
                    while (written < cb)
                    {
                        var toRead = bufSize;
                        if (written + toRead > cb)
                        {
                            toRead = (int)(cb - written);
                        }

                        var read = Read(buffer, toRead);
                        if (read == 0)
                        {
                            break;
                        }

                        if (psTm.Write(buffer, read) != read)
                        {
                            throw new ExternalException("Wrote an incorrect number of bytes", InteropValues.EFail);
                        }
                        written += read;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
                if (pcbRead is { Length: > 0 })
                {
                    pcbRead[0] = written;
                }

                return written;
            }

            public virtual Stream GetDataStream()
            {
                return DataStream;
            }

            public virtual void LockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            protected static void NotImplemented()
            {
                throw new NotImplementedException();
            }

            public virtual int Read(IntPtr buf, int length)
            {
                var buffer = new byte[length];
                var count = Read(buffer, length);
                Marshal.Copy(buffer, 0, buf, length);
                return count;
            }

            public virtual int Read(byte[] buffer, int length)
            {
                ActualizeVirtualPosition();
                return DataStream.Read(buffer, 0, length);
            }

            public virtual void Revert()
            {
                NotImplemented();
            }

            public virtual long Seek(long offset, int origin)
            {
                var pos = _virtualPosition;
                if (_virtualPosition == -1)
                {
                    pos = DataStream.Position;
                }
                var len = DataStream.Length;

                switch (origin)
                {
                    case StreamConst.StreamSeekSet:
                        if (offset <= len)
                        {
                            DataStream.Position = offset;
                            _virtualPosition = -1;
                        }
                        else
                        {
                            _virtualPosition = offset;
                        }
                        break;
                    case StreamConst.StreamSeekEnd:
                        if (offset <= 0)
                        {
                            DataStream.Position = len + offset;
                            _virtualPosition = -1;
                        }
                        else
                        {
                            _virtualPosition = len + offset;
                        }
                        break;
                    case StreamConst.StreamSeekCur:
                        if (offset + pos <= len)
                        {
                            DataStream.Position = pos + offset;
                            _virtualPosition = -1;
                        }
                        else
                        {
                            _virtualPosition = offset + pos;
                        }
                        break;
                }

                return _virtualPosition != -1 ? _virtualPosition : DataStream.Position;
            }

            public virtual void SetSize(long value)
            {
                DataStream.SetLength(value);
            }

            public virtual void Stat(IntPtr pstatstg, int grfStatFlag)
            {
                NotImplemented();
            }

            public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            public virtual int Write(IntPtr buf, int length)
            {
                var buffer = new byte[length];
                Marshal.Copy(buf, buffer, 0, length);
                return Write(buffer, length);
            }

            public virtual int Write(byte[] buffer, int length)
            {
                ActualizeVirtualPosition();
                DataStream.Write(buffer, 0, length);
                return length;
            }
        }
    }
}
