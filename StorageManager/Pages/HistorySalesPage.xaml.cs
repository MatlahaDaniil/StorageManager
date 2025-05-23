using Database.SQL.Models;
using StorageManager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StorageManager.Pages
{
    public partial class HistorySalesPage : Page
    {
        DbManager db = DbManager.Instance;
        public List<HistoryEntity> Histories { get; set; } = new List<HistoryEntity>();

        void CreateTable(List<ProductAndCustomer> list)
        {
            var dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                IsReadOnly = true,
                Margin = new Thickness(10),
                RowBackground = Brushes.LightGray,
                GridLinesVisibility = DataGridGridLinesVisibility.None,
                HeadersVisibility = DataGridHeadersVisibility.Column,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 14
            };


            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Назва",
                Binding = new System.Windows.Data.Binding("NameProduct"),
                Width = 250
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Закупівельна ціна",
                Binding = new System.Windows.Data.Binding("PurchasePrice") { StringFormat = "C" },
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Роздрібна ціна",
                Binding = new System.Windows.Data.Binding("Cost") { StringFormat = "C" },
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Кількість",
                Binding = new System.Windows.Data.Binding("Count"),
                Width = 100
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Ім'я користувача",
                Binding = new System.Windows.Data.Binding("Name"),
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Номер користувача",
                Binding = new System.Windows.Data.Binding("PhoneNumber"),
                Width = 150
            });

            var headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0, 122, 204))));
            headerStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.White));
            headerStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(Control.FontSizeProperty, 16.0));
            headerStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(5)));
            dataGrid.ColumnHeaderStyle = headerStyle;


            dataGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#36393F")!);
            dataGrid.FontSize = 20; // збільшення шрифту
            dataGrid.Margin = new Thickness(15); // відступи

            var rowStyle = new Style(typeof(DataGridRow));

            rowStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            rowStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.Bold));

            rowStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#36393F"))));

            var mouseOverTrigger = new Trigger
            {
                Property = DataGridRow.IsMouseOverProperty,
                Value = true
            };
            mouseOverTrigger.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Color.FromRgb(70, 75, 80))));
            rowStyle.Triggers.Add(mouseOverTrigger);

            var selectedTrigger = new Trigger
            {
                Property = DataGridRow.IsSelectedProperty,
                Value = true
            };
            selectedTrigger.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Color.FromRgb(51, 153, 255))));
            selectedTrigger.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.White));
            rowStyle.Triggers.Add(selectedTrigger);

            dataGrid.RowStyle = rowStyle;

            this.Content = dataGrid;

            dataGrid.ItemsSource = list;
        }
        void UpdateDataGrid()
        {
            List<ProductAndCustomer> list = new List<ProductAndCustomer>();
            foreach (var history in db.getAllHistories())
            {
                var product = db.get_Product(history.ProductId);
                var customer = db.get_Customer(history.CustomerId);

                if (product != null && customer != null)
                {
                    list.Add(new ProductAndCustomer
                    {
                        NameProduct = product.Name,
                        Description = product.Description,
                        Count = product.Count,
                        PurchasePrice = product.PurchasePrice,
                        Cost = product.Cost,
                        Image = product.Image,
                        Name = customer.Name,
                        PhoneNumber = customer.PhoneNumber
                    });
                }
            }
             CreateTable(list);
        }
        public HistorySalesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}
