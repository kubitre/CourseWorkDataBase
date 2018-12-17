using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Windows;

namespace AdminPanel.Views.User
{
    public partial class UserAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public UserAdd()
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

        private void CooperatorChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void RoleChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
