using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;

namespace AdminPanel.Views.Products
{
    /// <summary>
    /// Interaction logic for ProductsAdd.xaml
    /// </summary>
    public partial class ProductsAdd : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;

        public ProductsAdd()
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

        private void AdddProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(ChekcInput(this.NameProduct_input.Text, this.PriceProduct_input.Text))
            {
                var clientNetwork = new NetworkMiddleware.Client();
                try
                {
                    clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_CREATE_CODE, new NetworkMiddleware.NetworkData.Product
                    {
                        Name = this.NameProduct_input.Text,
                        Price = double.Parse(this.PriceProduct_input.Text)
                    });

                    this.DialogResult = true;
                }
                catch(Exception ex)
                {
                    this.DialogResult = false;
                    this.ShowMessageAsync("Ошибка!", $"Код ошибки: {ex.Message}");
                }
            }

            
        }

        private bool ChekcInput(params string[] param)
        {
            foreach (var element in param)
            {
                if (string.IsNullOrEmpty(element) | string.IsNullOrWhiteSpace(element))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
