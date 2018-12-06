using MahApps.Metro.Controls;

namespace AdminPanel
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CooperatorsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.MainWindowSSSS.Children.Clear();
        }

        private void DishesWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowDishes = new Dish();
            windowDishes.Show();
            this.Close();
        }

        private void MenusWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMenu = new Menu();
            windowMenu.Show();
            this.Hide();
        }

        private void UsersWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SettingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void exitFromSession_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MySettingsItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
