using ControlzEx.Behaviors;

using System.Reflection;

namespace Aak.Shell.UI.Themes.AvalonDock.Attachs
{
    internal static class BehaviorFactory
    {
        // See: https://github.com/ControlzEx/ControlzEx/blob/develop/src/ControlzEx/Behaviors/WindowChrome/WindowChromeBehavior.MessageHandling.cs#L447
        private sealed class FixWindowChromeBehavior : WindowChromeBehavior
        {
            protected override void OnAttached()
            {
                base.OnAttached();

                AssociatedObject.Loaded += AssociatedObject_Loaded;
            }

            private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
            {
                AssociatedObject.Loaded -= AssociatedObject_Loaded;

                var handleERASEBKGND = typeof(WindowChromeBehavior).GetField("handleERASEBKGND", BindingFlags.NonPublic | BindingFlags.Instance);
                handleERASEBKGND?.SetValue(this, false);
            }
        }

        public static GlowWindowBehavior CreateGlowWindowBehavior()
        => new() { IsGlowTransitionEnabled = true };

        public static WindowChromeBehavior CreateWindowChromeBehavior()
        => new FixWindowChromeBehavior();
    }
}
