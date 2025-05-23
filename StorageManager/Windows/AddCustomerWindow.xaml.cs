using Database.SQL.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        DbManager db = DbManager.Instance;
        ProductEntity product = null;
        public AddCustomerWindow(ProductEntity product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if ((Name_tbx.Text != "" || Name_tbx.Text != "Ім'я покупця") &&
                (Phone_tbx.Text != "" || Phone_tbx.Text != "Номер телефону") &&
                (Count_tbx.Text != "" || Count_tbx.Text != "Кількість товару") &&
                int.TryParse(Count_tbx.Text, out int result) == true)
            {
                if(result > product.Count)
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("Недостатня кількість товару на складі!");
                    customMessageBox.Owner = Application.Current.MainWindow;
                    customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    customMessageBox.ShowDialog();
                    return;
                }
                CustomerEntity customer = db.get_Customer(Phone_tbx.Text);
                if (customer == null)
                {
                    customer = new CustomerEntity()
                    {
                        Name = Name_tbx.Text,
                        PhoneNumber = Phone_tbx.Text
                    };
                    db.AddCustomer(customer);
                }
                HistoryEntity history = new HistoryEntity()
                {
                    ProductId = product.Id,
                    CustomerId = customer.Id,
                    ShopId = db.get_currentShop().Id
                };
                db.AddHistory(history);
                CustomMessageBox customMessageBox1 = new CustomMessageBox("Товар продано!");
                customMessageBox1.Owner = Application.Current.MainWindow;
                customMessageBox1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox1.ShowDialog();
                product.Count -= result;
                db.UpdateProduct(product);
                this.Close();
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Неправильно введені дані!");
                customMessageBox.Owner = Application.Current.MainWindow;
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customMessageBox.ShowDialog();
            }
        }

        private void Name_tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Name_tbx.Text == "" || Name_tbx.Text == "Ім'я покупця")
            {
                Name_tbx.Text = "";
            }
        }

        private void Name_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Name_tbx.Text == "" || Name_tbx.Text == "Ім'я покупця")
            {
                Name_tbx.Text = "Ім'я покупця";
            }
        }

        private void Phone_tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Phone_tbx.Text == "" || Phone_tbx.Text == "Номер телефону")
            {
                Phone_tbx.Text = "";
            }
        }

        private void Phone_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Phone_tbx.Text == "" || Phone_tbx.Text == "Номер телефону")
            {
                Phone_tbx.Text = "Номер телефону";
            }
        }

        private void Count_tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Count_tbx.Text == "" || Count_tbx.Text == "Кількість товару")
            {
                Count_tbx.Text = "";
            }
        }

        private void Count_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Count_tbx.Text == "" || Count_tbx.Text == "Кількість товару")
            {
                Count_tbx.Text = "Кількість товару";
            }
        }
    }
}
