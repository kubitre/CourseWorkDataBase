using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AdminPanel.Views.User
{
    public partial class PersonalSettings : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public PersonalSettings()
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
