using AvalonDock.Themes.VisualStudio.Behaviors;
using AvalonDock.Themes.VisualStudio.Helpers.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace AvalonDock.Themes.VisualStudio.Controls.GlowWindow
{
    internal class GlowEdge : HwndWrapper
    {
        private const string GLOW_EDGE_CLASS_NAME = "AvalonDock.Themes.VisualStudio.GlowWindow.GlowEdge";

        private const int GLOW_DEPTH = 9;

        public const int CornerGripThickness = 18;

        private static ushort _sharedWindowClassAtom;

        // ReSharper disable once NotAccessedField.Local
        internal static InteropValues.WndProc? SharedWndProc; /* Don't Remove it, bug */

        private readonly GlowBitmap[] _activeGlowBitmaps = new GlowBitmap[16];

        private readonly GlowBitmap[] _inactiveGlowBitmaps = new GlowBitmap[16];

        private readonly Dock _orientation;

        private readonly VisualStudioGlowWindowBehavior _behavior;

        private SolidColorBrush _activeGlowBrush = Brushes.Transparent;

        private int _height;

        private SolidColorBrush _inactiveGlowBrush = Brushes.Transparent;

        private FieldInvalidationTypes _invalidatedValues;

        private bool _isActive;

        private bool _isVisible;

        private int _left;

        private bool _pendingDelayRender;

        private int _top;

        private int _width;

        internal GlowEdge(VisualStudioGlowWindowBehavior owner, Dock orientation)
        {
            _behavior = owner ?? throw new ArgumentNullException(nameof(owner));
            _orientation = orientation;
            CreatedGlowEdges += 1L;
        }

        internal static long CreatedGlowEdges { get; private set; }

        internal static long DisposedGlowEdges { get; private set; }

        private bool IsDeferringChanges => _behavior.DeferGlowChangesCount > 0;

        private static ushort SharedWindowClassAtom
        {
            get
            {
                if (_sharedWindowClassAtom == 0)
                {
                    var wndclass = default(InteropValues.WndClass);

                    wndclass.cbClsExtra = 0;
                    wndclass.cbWndExtra = 0;
                    wndclass.hbrBackground = IntPtr.Zero;
                    wndclass.hCursor = IntPtr.Zero;
                    wndclass.hIcon = IntPtr.Zero;
                    wndclass.LpFnWndProc = SharedWndProc = InteropMethods.DefWindowProc;
                    wndclass.lpszClassName = GLOW_EDGE_CLASS_NAME;
                    wndclass.lpszMenuName = string.Empty;
                    wndclass.style = 0u;

                    _sharedWindowClassAtom = InteropMethods.RegisterClass(ref wndclass);
                }

                return _sharedWindowClassAtom;
            }
        }

        internal bool IsVisible
        {
            get => _isVisible;
            set => UpdateProperty(ref _isVisible, value, FieldInvalidationTypes.Render | FieldInvalidationTypes.Visibility);
        }

        internal int Left
        {
            get => _left;
            set => UpdateProperty(ref _left, value, FieldInvalidationTypes.Location);
        }

        internal int Top
        {
            get => _top;
            set => UpdateProperty(ref _top, value, FieldInvalidationTypes.Location);
        }

        internal int Width
        {
            get => _width;
            set => UpdateProperty(ref _width, value, FieldInvalidationTypes.Size | FieldInvalidationTypes.Render);
        }

        internal int Height
        {
            get => _height;
            set => UpdateProperty(ref _height, value, FieldInvalidationTypes.Size | FieldInvalidationTypes.Render);
        }

        internal bool IsActive
        {
            get => _isActive;
            set => UpdateProperty(ref _isActive, value, FieldInvalidationTypes.Render);
        }

        internal SolidColorBrush ActiveGlowBrush
        {
            get => _activeGlowBrush;
            set => UpdateProperty(ref _activeGlowBrush, value,
                FieldInvalidationTypes.ActiveColor | FieldInvalidationTypes.Render);
        }

        internal SolidColorBrush InactiveGlowBrush
        {
            get => _inactiveGlowBrush;
            set => UpdateProperty(ref _inactiveGlowBrush, value,
                FieldInvalidationTypes.InactiveColor | FieldInvalidationTypes.Render);
        }

        private IntPtr TargetWindowHandle => _behavior.GetWindowInteropHelper().Handle;

        internal override bool IsWindowSubclassed => true;

        private bool IsPositionValid =>
            (_invalidatedValues & (FieldInvalidationTypes.Location | FieldInvalidationTypes.Size |
                                   FieldInvalidationTypes.Visibility)) == FieldInvalidationTypes.None;

        private void UpdateProperty<T>(ref T field, T value, FieldInvalidationTypes invalidatedValues)
        {
            if (field != null && !field.Equals(value))
            {
                field = value;
                _invalidatedValues |= invalidatedValues;
                if (!IsDeferringChanges) CommitChanges();
            }
        }

        internal override ushort CreateWindowClassCore()
        {
            return SharedWindowClassAtom;
        }

        internal override void DestroyWindowClassCore()
        {
        }

        protected override IntPtr CreateWindowCore()
        {
            return InteropMethods.CreateWindowEx(
                524416,
                new IntPtr(WindowClassAtom),
                string.Empty,
                -2046820352,
                0,
                0,
                0,
                0,
                _behavior.GetWindowInteropHelper().Owner,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);
        }

        internal void ChangeOwner(IntPtr newOwner)
        {
            InteropMethods.SetWindowLongPtr(Handle, InteropValues.GwLp.HwNdParent, newOwner);
        }

        protected override IntPtr WndProc(IntPtr hwNd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg <= 70)
            {
                if (msg == 6) return IntPtr.Zero;

                if (msg == 70)
                {
                    var windowPos =
                        (InteropValues.WindowPos?)Marshal.PtrToStructure(lParam, typeof(InteropValues.WindowPos));
                    if (windowPos is not null)
                    {
                        windowPos.flags |= 16u;
                        Marshal.StructureToPtr(windowPos, lParam, true);
                    }
                }
            }
            else
            {
                if (msg != 126)
                {
                    if (msg == 132) return new IntPtr(WmNcHitTest(lParam));

                    switch (msg)
                    {
                        case 161:
                        case 163:
                        case 164:
                        case 166:
                        case 167:
                        case 169:
                        case 171:
                        case 173:
                            {
                                var targetWindowHandle = TargetWindowHandle;
                                InteropMethods.SendMessage(targetWindowHandle, 6, new IntPtr(2), IntPtr.Zero);
                                InteropMethods.SendMessage(targetWindowHandle, msg, wParam, IntPtr.Zero);
                                return IntPtr.Zero;
                            }
                    }
                }
                else
                {
                    if (IsVisible) RenderLayeredWindow();
                }
            }

            return base.WndProc(hwNd, msg, wParam, lParam);
        }

        private int WmNcHitTest(IntPtr lParam)
        {
            var xLParam = InteropMethods.GetXlParam(lParam.ToInt32());
            var yLParam = InteropMethods.GetYlParam(lParam.ToInt32());
            _ = InteropMethods.GetWindowRect(Handle, out var rect);
            switch (_orientation)
            {
                case Dock.Left:
                    if (yLParam - CornerGripThickness < rect.Top) return 13;

                    if (yLParam + CornerGripThickness > rect.Bottom) return 16;

                    return 10;
                case Dock.Top:
                    if (xLParam - CornerGripThickness < rect.Left) return 13;

                    if (xLParam + CornerGripThickness > rect.Right) return 14;

                    return 12;
                case Dock.Right:
                    if (yLParam - CornerGripThickness < rect.Top) return 14;

                    if (yLParam + CornerGripThickness > rect.Bottom) return 17;

                    return 11;
                default:
                    if (xLParam - CornerGripThickness < rect.Left) return 16;

                    if (xLParam + CornerGripThickness > rect.Right) return 17;

                    return 15;
            }
        }

        internal void CommitChanges()
        {
            InvalidateCachedBitmaps();
            UpdateWindowPosCore();
            UpdateLayeredWindowCore();
            _invalidatedValues = FieldInvalidationTypes.None;
        }

        private void InvalidateCachedBitmaps()
        {
            if (_invalidatedValues.HasFlag(FieldInvalidationTypes.ActiveColor)) ClearCache(_activeGlowBitmaps);

            if (_invalidatedValues.HasFlag(FieldInvalidationTypes.InactiveColor)) ClearCache(_inactiveGlowBitmaps);
        }

        private void UpdateWindowPosCore()
        {
            if (_invalidatedValues.HasFlag(FieldInvalidationTypes.Location) ||
                _invalidatedValues.HasFlag(FieldInvalidationTypes.Size) ||
                _invalidatedValues.HasFlag(FieldInvalidationTypes.Visibility))
            {
                var num = 532;
                if (_invalidatedValues.HasFlag(FieldInvalidationTypes.Visibility))
                {
                    if (IsVisible)
                        num |= 64;
                    else
                        num |= 131;
                }

                if (!_invalidatedValues.HasFlag(FieldInvalidationTypes.Location)) num |= 2;

                if (!_invalidatedValues.HasFlag(FieldInvalidationTypes.Size)) num |= 1;

                InteropMethods.SetWindowPos(Handle, new IntPtr(-1), Left, Top, Width, Height, num);
            }
        }

        private void UpdateLayeredWindowCore()
        {
            if (IsVisible && _invalidatedValues.HasFlag(FieldInvalidationTypes.Render))
            {
                if (IsPositionValid)
                {
                    BeginDelayedRender();
                    return;
                }

                CancelDelayedRender();
                RenderLayeredWindow();
            }
        }

        private void BeginDelayedRender()
        {
            if (!_pendingDelayRender)
            {
                _pendingDelayRender = true;
                CompositionTarget.Rendering += CommitDelayedRender;
            }
        }

        private void CancelDelayedRender()
        {
            if (_pendingDelayRender)
            {
                _pendingDelayRender = false;
                CompositionTarget.Rendering -= CommitDelayedRender;
            }
        }

        private void CommitDelayedRender(object? sender, EventArgs e)
        {
            CancelDelayedRender();
            if (IsVisible) RenderLayeredWindow();
        }

        private void RenderLayeredWindow()
        {
            using var glowDrawingContext = new GlowDrawingContext(Width, Height);
            if (glowDrawingContext.IsInitialized)
            {
                switch (_orientation)
                {
                    case Dock.Left:
                        DrawLeft(glowDrawingContext);
                        break;
                    case Dock.Top:
                        DrawTop(glowDrawingContext);
                        break;
                    case Dock.Right:
                        DrawRight(glowDrawingContext);
                        break;
                    default:
                        DrawBottom(glowDrawingContext);
                        break;
                }

                var point = new InteropValues.Point
                {
                    X = Left,
                    Y = Top
                };
                var size = new InteropValues.Size
                {
                    cx = Width,
                    cy = Height
                };
                var point2 = new InteropValues.Point
                {
                    X = 0,
                    Y = 0
                };

                InteropMethods.UpdateLayeredWindow(
                    Handle,
                    glowDrawingContext.ScreenDc,
                    ref point,
                    ref size,
                    glowDrawingContext.WindowDc,
                    ref point2,
                    0u,
                    ref glowDrawingContext.Blend,
                    2u);
            }
        }

        private GlowBitmap? GetOrCreateBitmap(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart)
        {
            GlowBitmap?[] array;
            Color color;
            if (IsActive)
            {
                array = _activeGlowBitmaps;
                color = ActiveGlowBrush.Color;
            }
            else
            {
                array = _inactiveGlowBitmaps;
                color = InactiveGlowBrush.Color;
            }

            return array[(int)bitmapPart] ??
                   (array[(int)bitmapPart] = GlowBitmap.Create(drawingContext, bitmapPart, color));
        }

        private static void ClearCache(GlowBitmap?[] cache)
        {
            for (var i = 0; i < cache.Length; i++)
                using (cache[i])
                {
                    cache[i] = null;
                }
        }

        protected override void DisposeManagedResources()
        {
            ClearCache(_activeGlowBitmaps);
            ClearCache(_inactiveGlowBitmaps);
        }

        protected override void DisposeNativeResources()
        {
            base.DisposeNativeResources();
            DisposedGlowEdges += 1L;
        }

        private void DrawLeft(GlowDrawingContext drawingContext)
        {
            GlowBitmap? orCreateBitmap;
            GlowBitmap? orCreateBitmap2;
            GlowBitmap? orCreateBitmap3;
            GlowBitmap? orCreateBitmap4;
            GlowBitmap? orCreateBitmap5;

            if ((orCreateBitmap = GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerTopLeft)) is null)
                return;
            if ((orCreateBitmap2 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.LeftTop)) is null)
                return;
            if ((orCreateBitmap3 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.Left)) is null)
                return;
            if ((orCreateBitmap4 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.LeftBottom)) is null)
                return;
            if ((orCreateBitmap5 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerBottomLeft)) is null)
                return;

            var height = orCreateBitmap.Height;
            var num = height + orCreateBitmap2.Height;
            var num2 = drawingContext.Height - orCreateBitmap5.Height;
            var num3 = num2 - orCreateBitmap4.Height;
            var num4 = num3 - num;
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.BackgroundDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.Blend);
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap2.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, height, orCreateBitmap2.Width,
                orCreateBitmap2.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap2.Width,
                orCreateBitmap2.Height, drawingContext.Blend);
            if (num4 > 0)
            {
                InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap3.Handle);
                InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num, orCreateBitmap3.Width, num4,
                    drawingContext.BackgroundDc, 0, 0, orCreateBitmap3.Width, orCreateBitmap3.Height,
                    drawingContext.Blend);
            }

            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap4.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num3, orCreateBitmap4.Width,
                orCreateBitmap4.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap4.Width,
                orCreateBitmap4.Height, drawingContext.Blend);
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap5.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num2, orCreateBitmap5.Width,
                orCreateBitmap5.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap5.Width,
                orCreateBitmap5.Height, drawingContext.Blend);
        }

        private void DrawRight(GlowDrawingContext drawingContext)
        {
            GlowBitmap? orCreateBitmap;
            GlowBitmap? orCreateBitmap2;
            GlowBitmap? orCreateBitmap3;
            GlowBitmap? orCreateBitmap4;
            GlowBitmap? orCreateBitmap5;

            if ((orCreateBitmap = GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerTopRight)) is null)
                return;
            if ((orCreateBitmap2 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.RightTop)) is null)
                return;
            if ((orCreateBitmap3 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.Right)) is null)
                return;
            if ((orCreateBitmap4 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.RightBottom)) is null)
                return;
            if ((orCreateBitmap5 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerBottomRight)) is null)
                return;

            var height = orCreateBitmap.Height;
            var num = height + orCreateBitmap2.Height;
            var num2 = drawingContext.Height - orCreateBitmap5.Height;
            var num3 = num2 - orCreateBitmap4.Height;
            var num4 = num3 - num;
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.BackgroundDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.Blend);
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap2.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, height, orCreateBitmap2.Width,
                orCreateBitmap2.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap2.Width,
                orCreateBitmap2.Height, drawingContext.Blend);
            if (num4 > 0)
            {
                InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap3.Handle);
                InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num, orCreateBitmap3.Width, num4,
                    drawingContext.BackgroundDc, 0, 0, orCreateBitmap3.Width, orCreateBitmap3.Height,
                    drawingContext.Blend);
            }

            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap4.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num3, orCreateBitmap4.Width,
                orCreateBitmap4.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap4.Width,
                orCreateBitmap4.Height, drawingContext.Blend);
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap5.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, 0, num2, orCreateBitmap5.Width,
                orCreateBitmap5.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap5.Width,
                orCreateBitmap5.Height, drawingContext.Blend);
        }

        private void DrawTop(GlowDrawingContext drawingContext)
        {
            GlowBitmap? orCreateBitmap;
            GlowBitmap? orCreateBitmap2;
            GlowBitmap? orCreateBitmap3;

            if ((orCreateBitmap = GetOrCreateBitmap(drawingContext, GlowBitmapPart.TopLeft)) is null)
                return;
            if ((orCreateBitmap2 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.Top)) is null)
                return;
            if ((orCreateBitmap3 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.TopRight)) is null)
                return;

            var num2 = GLOW_DEPTH + orCreateBitmap.Width;
            var num3 = drawingContext.Width - GLOW_DEPTH - orCreateBitmap3.Width;
            var num4 = num3 - num2;
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, GLOW_DEPTH, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.BackgroundDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.Blend);
            if (num4 > 0)
            {
                InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap2.Handle);
                InteropMethods.AlphaBlend(drawingContext.WindowDc, num2, 0, num4, orCreateBitmap2.Height,
                    drawingContext.BackgroundDc, 0, 0, orCreateBitmap2.Width, orCreateBitmap2.Height,
                    drawingContext.Blend);
            }

            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap3.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, num3, 0, orCreateBitmap3.Width,
                orCreateBitmap3.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap3.Width,
                orCreateBitmap3.Height, drawingContext.Blend);
        }

        private void DrawBottom(GlowDrawingContext drawingContext)
        {
            GlowBitmap? orCreateBitmap;
            GlowBitmap? orCreateBitmap2;
            GlowBitmap? orCreateBitmap3;

            if ((orCreateBitmap = GetOrCreateBitmap(drawingContext, GlowBitmapPart.BottomLeft)) is null)
                return;
            if ((orCreateBitmap2 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.Bottom)) is null)
                return;
            if ((orCreateBitmap3 = GetOrCreateBitmap(drawingContext, GlowBitmapPart.BottomRight)) is null)
                return;

            var num2 = GLOW_DEPTH + orCreateBitmap.Width;
            var num3 = drawingContext.Width - GLOW_DEPTH - orCreateBitmap3.Width;
            var num4 = num3 - num2;
            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, GLOW_DEPTH, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.BackgroundDc, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height,
                drawingContext.Blend);
            if (num4 > 0)
            {
                InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap2.Handle);
                InteropMethods.AlphaBlend(drawingContext.WindowDc, num2, 0, num4, orCreateBitmap2.Height,
                    drawingContext.BackgroundDc, 0, 0, orCreateBitmap2.Width, orCreateBitmap2.Height,
                    drawingContext.Blend);
            }

            InteropMethods.SelectObject(drawingContext.BackgroundDc, orCreateBitmap3.Handle);
            InteropMethods.AlphaBlend(drawingContext.WindowDc, num3, 0, orCreateBitmap3.Width,
                orCreateBitmap3.Height, drawingContext.BackgroundDc, 0, 0, orCreateBitmap3.Width,
                orCreateBitmap3.Height, drawingContext.Blend);
        }

        internal void UpdateWindowPos()
        {
            var targetWindowHandle = TargetWindowHandle;
            _ = InteropMethods.GetWindowRect(targetWindowHandle, out var rect);
            InteropMethods.GetWindowPlacement(targetWindowHandle);
            if (IsVisible)
                switch (_orientation)
                {
                    case Dock.Left:
                        Left = rect.Left - GLOW_DEPTH;
                        Top = rect.Top - GLOW_DEPTH;
                        Width = GLOW_DEPTH;
                        Height = rect.Height + CornerGripThickness;
                        return;
                    case Dock.Top:
                        Left = rect.Left - GLOW_DEPTH;
                        Top = rect.Top - GLOW_DEPTH;
                        Width = rect.Width + CornerGripThickness;
                        Height = GLOW_DEPTH;
                        return;
                    case Dock.Right:
                        Left = rect.Right;
                        Top = rect.Top - GLOW_DEPTH;
                        Width = GLOW_DEPTH;
                        Height = rect.Height + CornerGripThickness;
                        return;
                    default:
                        Left = rect.Left - GLOW_DEPTH;
                        Top = rect.Bottom;
                        Width = rect.Width + CornerGripThickness;
                        Height = GLOW_DEPTH;
                        break;
                }
        }

        [Flags]
        private enum FieldInvalidationTypes
        {
            None = 0,
            Location = 1,
            Size = 2,
            ActiveColor = 4,
            InactiveColor = 8,
            Render = 16,
            Visibility = 32
        }
    }
}