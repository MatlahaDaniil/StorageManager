using Database.SQL.Models;
using StorageManager.Database;
using StorageManager.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace StorageManager.Pages
{
    /// <summary>
    /// Interaction logic for StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        List<ProductEntity> products = null;
        DbManager db = DbManager.Instance;
        public StoragePage()
        {
            InitializeComponent();
            products = db.getAllProducts();
            fillWrap();
        }
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
                catch(Exception ex)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/unknown_img.png"));
                }

            }
            return bitmap;
        }
        Border CreateProductElement(ProductEntity product)
        {
            var border = new Border
            {
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.White,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                Background = Brushes.Transparent,
                Width = 200,
                Height = 300,
                MaxWidth = 200,
                MaxHeight = 300,
                VerticalAlignment = VerticalAlignment.Top,
                Style = (Style)this.Resources["ProductCardStyle"]
            };

            var grid = new Grid
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 

            
                var image = new Image
                {
                    Source = LoadImage(product.Image),
                    Height = 100,
                    Width = 100,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 5),
                    Style = (Style)this.Resources["ProductImageStyle"]
                };
                Grid.SetRow(image, 0);
                grid.Children.Add(image);
            

            if (product.Name.Length < 15)
            {
                var nameText = new TextBlock
                {
                    Text = product.Name,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 5, 0, 2),
                    Style = (Style)this.Resources["ProductNameTextStyle"]
                };
                Grid.SetRow(nameText, 1);
                grid.Children.Add(nameText);
            }
            else
            {
                string name = product.Name.Substring(0, 15) + "...";
                var nameText = new TextBlock
                {
                    Text = name,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 5, 0, 2),
                    Style = (Style)this.Resources["ProductNameTextStyle"]
                };
                Grid.SetRow(nameText, 1);
                grid.Children.Add(nameText);
            }

            if (product.Description.Length < 50)
            {
                var descriptionText = new TextBlock
                {
                    Text = product.Description,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 0, 0, 5),
                    Style = (Style)this.Resources["ProductDescriptionTextStyle"]
                };
                Grid.SetRow(descriptionText, 2);
                grid.Children.Add(descriptionText);
            }
            else
            {
                string desc = product.Description.Substring(0, 50) + "...";
                var descriptionText = new TextBlock
                {
                    Text = desc,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 0, 0, 5),
                    Style = (Style)this.Resources["ProductDescriptionTextStyle"]
                };
                Grid.SetRow(descriptionText, 2);
                grid.Children.Add(descriptionText);
            }      

            var costText = new TextBlock
            {
                Text = $"Cost: {product.Cost:C}",
                FontSize = 14,
                FontStyle = FontStyles.Italic,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 5),
                Style = (Style)this.Resources["ProductCostTextStyle"]
            };
            Grid.SetRow(costText, 3);
            grid.Children.Add(costText);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0)
            };

            var editButton = new Button
            {
                Content = "Змінити",
                Margin = new Thickness(5),
                Width = 80,
                Height = 30,
                FontWeight = FontWeights.Bold,
                Cursor = System.Windows.Input.Cursors.Hand,
                Style = (Style)this.Resources["btn_ForStyleTestManager"]
            };

            var deleteButton = new Button
            {
                Content = "Вилучити",
                Margin = new Thickness(5),
                Width = 80,
                Height = 30,
                FontWeight = FontWeights.Bold,
                Cursor = System.Windows.Input.Cursors.Hand,
                Style = (Style)this.Resources["btn_ForStyleTestManager"]
            };

            editButton.Click += (s, e) => EditProduct(product.Id);
            deleteButton.Click += (s, e) => DeleteProduct(product.Id);
            border.MouseDown += (s, e) => CheckProduct(product.Id);

            buttonPanel.Children.Add(deleteButton);
            buttonPanel.Children.Add(editButton);

            Grid.SetRow(buttonPanel, 4);
            grid.Children.Add(buttonPanel);

            border.Child = grid;
            return border;
        }
        void fillWrap()
        {
            MainWrapPanel.Children.Clear();
            products.Clear();
            products = db.getAllProducts();
            foreach (var product in products)
            {
                MainWrapPanel.Children.Add(CreateProductElement(product));
            }
        }

        private void EditProduct(Guid id)
        {
            EditProductWindow editProductWindow = new EditProductWindow(db.get_Product(id));
            editProductWindow.Owner = Application.Current.MainWindow;
            editProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            editProductWindow.ShowDialog();
            products = db.getAllProducts();
            fillWrap();
        }
        private void CheckProduct(Guid id)
        {
            AboutProductWindow aboutProductWindow = new AboutProductWindow(db.get_Product(id));
            aboutProductWindow.Owner = Application.Current.MainWindow;
            aboutProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutProductWindow.ShowDialog();
        }
        private void DeleteProduct(Guid id)
        {
            if (db.DeleteProductById(id)) {
                CustomMessageBox customMessageBox = new CustomMessageBox("Товар успішно вилучено");
                customMessageBox.Owner = Application.Current.MainWindow;
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox.ShowDialog();
                fillWrap();
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Помилка при вилученні товару");
                customMessageBox.Owner = Application.Current.MainWindow;
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox.ShowDialog();
            }
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWrapPanel.Children.Clear();
            List<ProductEntity>searchedProducts = db.get_Products(search_txb.Text);
            if (searchedProducts.Count == 0)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Товар не знайдено");
                customMessageBox.Owner = Application.Current.MainWindow;
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox.ShowDialog();
                fillWrap();
            }
            else
            {
                foreach (var product in searchedProducts)
                {
                    MainWrapPanel.Children.Add(CreateProductElement(product));
                }
            }
        }

        private void AddProduct_btn_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.Owner = Application.Current.MainWindow;
            addProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addProductWindow.ShowDialog();
            products = db.getAllProducts();
            fillWrap();
        }
    }
}
