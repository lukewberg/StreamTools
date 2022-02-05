using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StreamTools.Views
{
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
