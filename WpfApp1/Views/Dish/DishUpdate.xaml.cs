using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AdminPanel.Views.Dish
{
    /// <summary>
    /// Interaction logic for DishUpdate.xaml
    /// </summary>
    public partial class DishUpdate : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        public DishUpdate()
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

        public void SetItem(NetworkMiddleware.NetworkData.Dish item)
        {
            this.DataContext = item;
        }

        private void UpdateDish_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ChooseProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ProductList_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CookcerList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
