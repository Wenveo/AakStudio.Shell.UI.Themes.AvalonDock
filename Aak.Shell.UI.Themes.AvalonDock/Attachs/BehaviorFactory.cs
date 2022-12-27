using ControlzEx.Behaviors;

namespace Aak.Shell.UI.Themes.AvalonDock.Attachs
{
    internal static class BehaviorFactory
    {
        public static GlowWindowBehavior CreateGlowWindowBehavior()
        => new() { IsGlowTransitionEnabled = true };

        public static WindowChromeBehavior CreateWindowChromeBehavior()
        => new();
    }
}
