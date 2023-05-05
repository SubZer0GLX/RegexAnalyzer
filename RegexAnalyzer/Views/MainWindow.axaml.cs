using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RegexAnalyzer.Views
{
    public partial class MainWindow : FluentWindow //Herda atributos da classe fluentwindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();

        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}