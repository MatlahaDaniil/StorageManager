using Database.SQL.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace StorageManager.Windows
{
    public partial class AboutProductWindow : Window
    {
        private ImageSource LoadImage(byte[] imageData)
        {
            var bitmap = new BitmapImage();
            if (imageData == null || imageData.Length == 0)
            {
                return new BitmapImage(new Uri("pack://application:,,,/Resources/unknown_img.png"));
            }
            using (var stream = new System.IO.MemoryStream(imageData))
            {
                try
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                catch (Exception ex)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/unknown_img.png"));
                }

            }
            return bitmap;
        }

        ProductEntity product = null;
        public AboutProductWindow(ProductEntity product)
        {
            InitializeComponent();
            this.product = product;

            Name_txb.Text = product.Name;
            Description_txb.Text = product.Description;
            Cost_txb.Text = product.Cost.ToString();
            Purch_Cost_txb.Text = product.PurchasePrice.ToString();
            Count_txb.Text = product.Count.ToString();
            ProductImage.Source = LoadImage(product.Image);
        }

        private void Sell_btn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow(product);
            addCustomerWindow.Owner = Application.Current.MainWindow;
            addCustomerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCustomerWindow.ShowDialog();
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
