using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminPanel.Views.Position
{
    /// <summary>
    /// Interaction logic for PositionUpdate.xaml
    /// </summary>
    public partial class PositionUpdate : MetroWindow
    {
        public PositionUpdate()
        {
            InitializeComponent();
        }

        private void UpdatePosition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
