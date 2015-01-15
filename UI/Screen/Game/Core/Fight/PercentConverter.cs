using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UI.Screen.Game.Core.Fight
{
    class PercentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) return DependencyProperty.UnsetValue;
            var healthPoint = System.Convert.ToInt32(values[0]);
            var initHealthPoint = System.Convert.ToInt32(values[1]);

            return Math.Round(((float) healthPoint / initHealthPoint) * 100d);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
