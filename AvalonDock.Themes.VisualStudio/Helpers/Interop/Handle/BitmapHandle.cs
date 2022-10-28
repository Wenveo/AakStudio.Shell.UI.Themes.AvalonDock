using System.Security;

namespace AvalonDock.Themes.VisualStudio.Helpers.Interop.Handle
{
    internal sealed class BitmapHandle : WpfSafeHandle
    {
        [SecurityCritical]
        internal BitmapHandle(bool ownsHandle) : base(ownsHandle, CommonHandles.Gdi)
        {
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return InteropMethods.DeleteObject(handle);
        }
    }
}