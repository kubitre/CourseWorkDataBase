using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Windows;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for ErrorConnect.xaml
    /// </summary>
    public partial class ErrorConnect : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public ErrorConnect()
        {
            InitializeComponent();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this._memory.AddToHistory("LoginPage");
            this.DialogResult = true;
        }
    }
}
