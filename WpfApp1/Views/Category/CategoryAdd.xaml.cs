using MahApps.Metro;
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

namespace AdminPanel.Views.Category
{
    /// <summary>
    /// Interaction logic for CategoryAdd.xaml
    /// </summary>
    public partial class CategoryAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private bool isUpdate = false;

        public CategoryAdd()
        {
            InitializeComponent();
        }

        public CategoryAdd(NetworkMiddleware.NetworkData.Category category)
        {
            InitializeComponent();
            this.Category_Input.Text = category.Name;
            this.Title = "Обновление категории";

            this.AddNewCategory.Content = "Обновить";
            isUpdate = true;
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent(this._memory.GetAppAccentTheme()),
                                        ThemeManager.GetAppTheme(this._memory.GetAppTheme()));
        }

        private void AddNewCategory_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.Category_Input.Text) | string.IsNullOrWhiteSpace(this.Category_Input.Text))
            {
                var clientNetwork = new NetworkMiddleware.Client();
                if(isUpdate)
                {
                    if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_UPDATE_CODE, this.Category_Input.Text))
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.DialogResult = false;
                    }
                }
                else
                {
                    if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CategoryCodes.CATEGORY_CREATE_CODE, this.Category_Input.Text))
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.DialogResult = false;
                    }
                }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
