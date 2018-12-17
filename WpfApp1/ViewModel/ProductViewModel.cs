using AdminPanel.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminPanel.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product selectedProduct;
        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ProductViewModel()
        {
            this.Products = new ObservableCollection<Product>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
