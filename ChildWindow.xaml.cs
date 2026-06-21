using System.Diagnostics;
using System.Windows;

namespace Family
{
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();

            ChildProcessIdTextBlock.Text = Process.GetCurrentProcess().Id.ToString();
        }
    }
}