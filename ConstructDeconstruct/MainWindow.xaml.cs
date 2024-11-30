using ConstructDeconstruct.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ConstructDeconstruct
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = new ObservableCollection<Product>();
                return _products;
            }
            set
            {
                Products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            ProductList.SetBinding(ListBox.ItemsSourceProperty, new Binding()
            {
                Source = Products
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            string productName = ProductName.Text.Trim();
            float productPrice = float.TryParse(ProductPrice.Text, out productPrice) ? productPrice : 0;
            int productQuantity = int.TryParse(ProductQuantity.Text, out productQuantity) ? productQuantity : 0;

            if (!string.IsNullOrEmpty(productName) && productPrice != 0 && productQuantity != 0)
            {
                Products.Add(new Product(productName, productPrice, productQuantity));
            }
            else if (!string.IsNullOrEmpty(productName) && productPrice != 0 && productQuantity == 0)
            {
                Products.Add(new Product(productName, productPrice));
            }

            ClearAll();
        }

        private void ClearAll()
        {
            ProductName.Clear();
            ProductPrice.Clear();
            ProductQuantity.Clear();
        }


        private void ShowProduct(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem == null) return;

            if (ProductDataView.Visibility == Visibility.Visible) // For visual purposes only
            {
                ProductDataView.Visibility = Visibility.Collapsed;
            }
            else
            {
                Products[ProductList.SelectedIndex].Deconstruct(out string productName, out double productPrice, out int productQuantity);

                ProductNameView.Text = productName;
                ProductPriceView.Text = productPrice.ToString();
                ProductQuantityView.Text = productQuantity.ToString();

                ProductDataView.Visibility = Visibility.Visible;
            }
        }

        // For visual purposes only 
        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductDataView.Visibility = Visibility.Collapsed;
        }
    }
}
