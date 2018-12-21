using AdminPanel.Views.Cooperator;
using AdminPanel.Views.Dish;
using AdminPanel.Views.Menu;
using AdminPanel.Views.Products;
using AdminPanel.Views.Streets;
using AdminPanel.Views.User;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
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
                    //Main functionality
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Пользователи", this.UsersWindow_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Сотрудники", this.CooperatorsWindow_Click));

                    //Dictionaries
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Position.Position().Name, "Должности", this.PositionWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Категории", this.CategoryWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Улицы", this.StreetsWindow_Click));
                    break;

                case 1:
                    //main functionality
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Пользователи", this.UsersWindow_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.WorkBook.WorkBook().Name, "Трудовая книга", this.WorkBook_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Сотрудники", this.CooperatorsWindow_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Dish.Dish().Name, "Блюда", this.DishesWindow_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Menu.Menu().Name, "Меню", this.MenusWindow_Click));

                    //Dictionaries
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Position.Position().Name, "Должности", this.PositionWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Категории", this.CategoryWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Улицы", this.StreetsWindow_Click));

                    break;

                case 2:
                    //main functionality
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Menu.Menu().Name, "Меню", this.MenusWindow_Click));
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Dish.Dish().Name, "Блюда", this.DishesWindow_Click));
                    this.TraceRoute.Content = "Клиентская панель";
                    //dictionaries
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Products.Products().Name, "Продукты", this.ProductsWindow_Click));

                    break;
                case 3:
                    //main functionality
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Cooperator.Cooperator().Name, "Сотрудники", this.CooperatorsWindow_Click));
                    this.TraceRoute.Content = "Клиентская панель";
                    //TODO: WORKBOOKS


                    //dictionaries
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Category.Category().Name, "Категории", this.CategoryWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Position.Position().Name, "Должности", this.PositionWindow_Click));
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Streets.Street().Name, "Улицы", this.StreetsWindow_Click));

                    break;
                case 4:
                    //main functionality
                    this.MainMenu.Items.Add(GetButtonByClass(new Views.Dish.Dish().Name, "Блюда", this.DishesWindow_Click));
                    this.TraceRoute.Content = "Клиентская панель";

                    //dictionaries
                    this.DictionaryMenu.Items.Add(GetButtonByClass(new Views.Products.Products().Name, "Продукты", this.ProductsWindow_Click));
                    break;
            }
        }

        private Button GetButtonByClass(string nameClass, string contextName, System.Windows.RoutedEventHandler handler)
        {
            var button = new Button();
            button.Name = nameClass;
            button.Click += handler;
            button.Margin = new System.Windows.Thickness(5);
            button.Padding = new System.Windows.Thickness(80);
            button.FontSize = 25;
            button.FontFamily = new FontFamily("Times New Roman");
            button.Background = Brushes.Orange;
            button.Content = contextName;
            return button;
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            this.UserName.Header = this._memory.GetUserName();
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
            this.roleUser.Text = this._memory.GetuserRoleOnRussian();
            this.Title = this._memory.GetTypeApplication();
            this.AddMenuItemsByRoleAccess(Models.Role.GetIndexRole(this._memory.GetUserRole()));
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

        private void WorkBook_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowWithWorkBooks = new Views.WorkBook.WorkBook();
            this._memory.AddToHistory("WorkBookPanel");
            windowWithWorkBooks.SetMemoryDump(this._memory);
            windowWithWorkBooks.Show();
            this.Close();
        }

        private void PositionWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowDishes = new Views.Position.Position();
            this._memory.AddToHistory("DishPanel");
            windowDishes.SetMemoryDump(this._memory);
            windowDishes.Show();
            this.Close();
        }

        private void CategoryWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowDishes = new Views.Category.Category();
            this._memory.AddToHistory("CategoryPanel");
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
                GetMessageAuth("Были применены новые настройки");
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
            {
                this._memory.ChangeAppTheme("BaseLight");
                this.roleUser.Foreground = Brushes.Black;
            }
            else
            {
                this._memory.ChangeAppTheme("BaseDark");
                this.roleUser.Foreground = Brushes.White;
            }
            

            ThemeManager.ChangeAppStyle(this,
                                                ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                                ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }
    }
}
