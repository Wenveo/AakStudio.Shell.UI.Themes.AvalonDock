namespace AvalonDock.Themes.VisualStudio.Helpers.Interop.Handle
{
    internal static class CommonHandles
    {
        public static readonly int Icon = HandleCollector.RegisterType(nameof(Icon), 20, 500);

        public static readonly int Hdc = HandleCollector.RegisterType(nameof(Hdc), 100, 2);

        public static readonly int Gdi = HandleCollector.RegisterType(nameof(Gdi), 50, 500);

        public static readonly int Kernel = HandleCollector.RegisterType(nameof(Kernel), 0, 1000);
    }
}