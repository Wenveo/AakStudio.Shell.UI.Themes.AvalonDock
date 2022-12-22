using AvalonDock.Controls;
using AvalonDock.Layout;
using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace AvalonDock.Themes.VisualStudio.Converters
{
    [ValueConversion(typeof(LayoutAnchorGroupControl), typeof(double))]
    public class AnchorSideToAngleConverter2 : MarkupExtension, IValueConverter
    {
        private static AnchorSideToAngleConverter2? instance;

        public static AnchorSideToAngleConverter2 Instance => instance ??= new AnchorSideToAngleConverter2();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Fix Error Binding
            if (value is LayoutAnchorGroupControl groupControl &&
                groupControl.Model is LayoutAnchorGroup anchorGroup &&
                anchorGroup.Parent is LayoutAnchorSide anchorSide &&
                (anchorSide.Side == AnchorSide.Left || anchorSide.Side == AnchorSide.Right))
            {
                return 90.0;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}