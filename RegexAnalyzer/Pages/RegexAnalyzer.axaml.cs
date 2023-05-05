using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RegexAnalyzer.Pages
{
    public partial class RegexAnalyzer : UserControl
    {
        public RegexAnalyzer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
