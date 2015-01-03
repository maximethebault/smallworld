using System;
using System.Windows.Markup;

namespace UI
{
    public class SelectedMapToVisibility : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Console.Write(serviceProvider);
            return null;
        }
    }
}