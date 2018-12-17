using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;

namespace AdminPanel.Views.Products
{
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
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.ProductCodes.PRODUCT_CREATE_CODE, this.NameProduct_input.Text, this.PriceProduct_input.Text))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
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
