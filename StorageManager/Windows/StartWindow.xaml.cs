using Database.SQL.Models;
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
    public partial class StartWindow : Window
    {
        DbManager db;
        public StartWindow()
        {
            InitializeComponent();
            db = DbManager.Instance;
        }
        private void Username_txb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Username_txb.Text == "" || Username_txb.Text == "Логін")
            {
                Username_txb.Text = "Логін";
            }
        }

        private void Username_txb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Username_txb.Text == "" || Username_txb.Text == "Логін")
            {
                Username_txb.Text = "";
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password_pb.Password == "password")
            {
                Password_pb.Password = "";
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password_pb.Password == "" || Password_pb.Password == "password")
            {
                Password_pb.Password = "password";
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LogIn_btn_Click(object sender, RoutedEventArgs e)
        {
            if (db.CheckShop(new ShopEntity { Name = Username_txb.Text, Password = Password_pb.Password }))
            {
                this.Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            ShopEntity newShop = new ShopEntity { Name = Username_txb.Text, Password = Password_pb.Password };
            db.AddNewShop(newShop);
            this.Close();
        }
    }
}
