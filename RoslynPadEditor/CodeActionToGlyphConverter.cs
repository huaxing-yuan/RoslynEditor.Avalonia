using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CodeActions;
using RoslynPad.Roslyn;
using RoslynPad.Roslyn.CodeActions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynPadEditor;

public sealed class CodeActionToGlyphConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((CodeAction)value).GetGlyph().ToImageSource();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
