using Database.SQL.Models;
using Microsoft.Win32;
using StorageManager.Database;
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

namespace StorageManager.Windows
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        DbManager db = DbManager.Instance;
        string selectedFilePath = string.Empty;
        ProductEntity product;
        public EditProductWindow(ProductEntity product)
        {
            InitializeComponent();
            this.product = product;

            NameTextBox.Text = product.Name;
            DescriptionTextBox.Text = product.Description;
            CountTextBox.Text = product.Count.ToString();
            PurchasePriceTextBox.Text = product.PurchasePrice.ToString();
            CostTextBox.Text = product.Cost.ToString();
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                path_txb.Text = selectedFilePath;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedFilePath == string.Empty)
                {
                    db.UpdateProduct(new ProductEntity
                    {
                        Id = product.Id,
                        Name = NameTextBox.Text,
                        Description = DescriptionTextBox.Text,
                        Count = int.Parse(CountTextBox.Text),
                        PurchasePrice = float.Parse(PurchasePriceTextBox.Text),
                        Cost = float.Parse(CostTextBox.Text),
                        Image = product.Image,
                        ShopId = db.get_currentShop().Id
                    });
                }
                else
                {
                    db.UpdateProduct(new ProductEntity
                    {
                        Id = product.Id,
                        Name = NameTextBox.Text,
                        Description = DescriptionTextBox.Text,
                        Count = int.Parse(CountTextBox.Text),
                        PurchasePrice = float.Parse(PurchasePriceTextBox.Text),
                        Cost = float.Parse(CostTextBox.Text),
                        Image = System.IO.File.ReadAllBytes(selectedFilePath),
                        ShopId = db.get_currentShop().Id
                    });
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Помилка редагування!");
                customMessageBox.Owner = Application.Current.MainWindow;
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox.ShowDialog();
                return;
            }
            this.Close();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
