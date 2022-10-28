using AvalonDock.Themes.VisualStudio.Behaviors;

namespace AvalonDock.Themes.VisualStudio.Controls.GlowWindow
{
    internal class ChangeScope : DisposableObject
    {
        private readonly VisualStudioGlowWindowBehavior _behavior;

        public ChangeScope(VisualStudioGlowWindowBehavior behavior)
        {
            _behavior = behavior;
            _behavior.DeferGlowChangesCount++;
        }

        protected override void DisposeManagedResources()
        {
            _behavior.DeferGlowChangesCount--;
            if (_behavior.DeferGlowChangesCount == 0) _behavior.EndDeferGlowChanges();
        }
    }
}