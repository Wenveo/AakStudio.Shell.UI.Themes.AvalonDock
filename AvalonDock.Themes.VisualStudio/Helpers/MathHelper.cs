using System;

namespace AvalonDock.Themes.VisualStudio.Helpers
{
    internal static class MathHelper
    {
        public static bool AreClose(double value1, double value2)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return value1 == value2 || IsVerySmall(value1 - value2);
        }

        public static bool IsVerySmall(double value)
        {
            return Math.Abs(value) < 1E-06;
        }
    }
}