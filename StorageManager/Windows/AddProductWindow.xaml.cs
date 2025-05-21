using Database.SQL.Models;
using Microsoft.Win32;
using StorageManager.Database;
using System.Windows;


namespace StorageManager.Windows
{
    public partial class AddProductWindow : Window
    {
        DbManager db = DbManager.Instance;
        string selectedFilePath = string.Empty;
        public AddProductWindow()
        {
            InitializeComponent();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.AddProduct(new ProductEntity { Name = NameTextBox.Text,
                                                  Description = DescriptionTextBox.Text,
                                                  Count = int.Parse(CountTextBox.Text),
                                                  PurchasePrice = float.Parse(PurchasePriceTextBox.Text),
                                                  Cost = float.Parse(CostTextBox.Text),
                                                  Image = System.IO.File.ReadAllBytes(selectedFilePath),
                                                  ShopId = db.get_currentShop().Id
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
