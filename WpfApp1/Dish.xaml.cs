using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for Dish.xaml
    /// </summary>
    public partial class Dish : MetroWindow
    {
        public Dish()
        {
            InitializeComponent();
            DataContext = new ViewModel.DishViewModel();
        }

        private void DishData_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as ViewModel.DishViewModel).Dishes.Add(new Models.Dish { Name = "Борщ", Recipe = "Тут какой-то рецепт", OutPrice = "123", Id = Guid.NewGuid() });       
        }

        private void ChangeElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RemoveElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AddNewElement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowAddingNewElement = new DishAdd();
            windowAddingNewElement.ShowDialog();
            
        }

        private void TraceRoute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var windowMain = new MainWindow();
            windowMain.Show();
            this.Close();
        }
    }
}
