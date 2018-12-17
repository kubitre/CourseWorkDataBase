using AdminPanel.Views.Cooperator;
using AdminPanel.Views.Dish;
using AdminPanel.Views.Menu;
using AdminPanel.Views.Products;
using AdminPanel.Views.Streets;
using AdminPanel.Views.User;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;
using Menu = AdminPanel.Views.Menu.Menu;

namespace AdminPanel
{
    public partial class MainWindow : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddMenuItemsByRoleAccess(int role)
        {
            switch (role)
            {
                case 0:
                    
                    break;

                case 1:

                    
                    break;

                case 2:

                    
                    break;
            }
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            this.UserName.Header = this._memory.GetUserName();
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
            this.roleUser.Text = this._memory.GetUserRole();
        }

        private void CooperatorsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var cooperatorWindow = new Cooperator();
            this._memory.AddToHistory("CooperatorPanel");
            cooperatorWindow.SetMemoryDump(this._memory);
            cooperatorWindow.Show();
            this.Close();
        }

        private void DishesWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowDishes = new Dish();
            this._memory.AddToHistory("DishPanel");
            windowDishes.SetMemoryDump(this._memory);
            windowDishes.Show();
            this.Close();
        }

        private void MenusWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMenu = new Menu();
            this._memory.AddToHistory("MenuPanel");
            windowMenu.SetMemoryDump(this._memory);
            windowMenu.Show();
            this.Hide();
        }

        private void UsersWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var userWindow = new Users();
            this._memory.AddToHistory("UsersPanel");
            userWindow.SetMemoryDump(this._memory);
            userWindow.Show();
            this.Close();
        }

        private void SettingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void exitFromSession_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var loginPageWindo = new LoginPage();
            this._memory.AddToHistory("LoginPage");
            loginPageWindo.SetMemoryDump(this._memory);
            loginPageWindo.Show();
            this.Close();
        }

        private void MySettingsItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var SettingWindow = new AdminPanel.Views.User.PersonalSettings();
            this._memory.AddToHistory("PersonalSetting");
            SettingWindow.SetMemoryDump(this._memory);
            
            if(SettingWindow.ShowDialog() == true)
            {
                GetMessageAuth("Были применены новые н");
            }

        }
        private async void GetMessageAuth(string mess) => await this.ShowMessageAsync("", mess);

        private void SettingsPanels_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SettingsPanel.IsOpen = true;
        }

        private void AboutApplication_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutApp.IsOpen = true;
        }

        private void AboutMeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AboutMe.IsOpen = true;
        }

        private void ProductsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var productsWindow = new Products();
            this._memory.AddToHistory("Products");
            productsWindow.SetMemoryDump(this._memory);
            productsWindow.Show();
            this.Close();

        }

        private void StreetsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var streetWindows = new Street();
            this._memory.AddToHistory("Streets");
            streetWindows.SetMemoryDump(this._memory);
            streetWindows.Show();
            this.Close();
        }

        private void SwithcTheme_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = (sender as ToggleSwitch);
            if ((bool)toggleSwitch.IsChecked)
                this._memory.ChangeAppTheme("BaseLight");
            else
                this._memory.ChangeAppTheme("BaseDark");
            

            ThemeManager.ChangeAppStyle(this,
                                                ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                                ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
    }
}
