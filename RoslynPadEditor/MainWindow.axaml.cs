using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Scripting;
using RoslynPad.Editor;
using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RoslynPadEditor
{
    public partial class MainWindow : Window
    {
        private readonly RoslynHost _host;
        DocumentId documentId;

        public MainWindow()
        {
            InitializeComponent();

            _host = new RoslynHost(additionalAssemblies: new[]
            {
                    Assembly.Load("RoslynPad.Roslyn.Avalonia"),
                    Assembly.Load("RoslynPad.Editor.Avalonia"),
                    typeof(object).Assembly, //System.Runtime.dll
                    typeof(System.Text.RegularExpressions.Regex).Assembly, //System.Text.RegularExpressions.dll
                    typeof(Enumerable).Assembly, //System.Linq.dll
                    typeof(Console).Assembly, //System.Console.dll
                    typeof(UTF8Encoding).Assembly, //System.Text.Encoding.Extensions.dll
                    typeof(XmlElement).Assembly, //System.Xml.ReaderWriter.dll
                    typeof(Cookie).Assembly, //System.Net.Primitives.dll
                }, RoslynHostReferences.NamespaceDefault.With(assemblyReferences: new[]
            {
            typeof(object).Assembly, //System.Runtime.dll
            typeof(System.Text.RegularExpressions.Regex).Assembly, //System.Text.RegularExpressions.dll
            typeof(Enumerable).Assembly, //System.Linq.dll
            typeof(Console).Assembly, //System.Console.dll
            typeof(UTF8Encoding).Assembly, //System.Text.Encoding.Extensions.dll
            typeof(XmlElement).Assembly, //System.Xml.ReaderWriter.dll
            typeof(Cookie).Assembly, //System.Net.Primitives.dll
        }), disabledDiagnostics: ImmutableArray.Create("CS1701", "CS1702", "CS7011", "CS8097"));

        }


        private async void OnEditor_Loaded(object sender, RoutedEventArgs e)
        {
            txtCode.Focus();
            var workingDirectory = Directory.GetCurrentDirectory();
            documentId = await txtCode.InitializeAsync(_host, new ClassificationHighlightColors(),
                workingDirectory, string.Empty, SourceCodeKind.Script).ConfigureAwait(true);
        }

        private void BtnRun_Tapped(object? sender, TappedEventArgs e)
        {
            var script = txtCode.Text;
            var app = CSharpScript.Create(script, ScriptOptions.Default.WithReferences(_host.DefaultReferences).WithImports(_host.DefaultImports));
            var diag = app.Compile();

        }

    }
}
