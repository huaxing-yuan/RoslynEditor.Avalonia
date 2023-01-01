using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CodeActions;
using RoslynPad.Roslyn.CodeActions;
using System;
using System.Globalization;

namespace RoslynPadEditor
{
    public sealed class CodeActionsConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ((CodeAction)value).GetCodeActions();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
