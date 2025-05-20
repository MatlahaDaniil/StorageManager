using Database.SQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StorageManager.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    public partial class EditProfilePage : Page
    {
        DbManager db = DbManager.Instance;
        ShopEntity shop = null;
        string selectedFilePath;
        public event Action ProfileUpdated;
        public EditProfilePage()
        {
            InitializeComponent();
        }

        public byte[] ConvertLocalFileToBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            return File.ReadAllBytes(filePath);
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

        private void ShopLogo_img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                ShopLogo_img.Source = new BitmapImage(new Uri(selectedFilePath));
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            byte[] fileBytes = ConvertLocalFileToBytes(selectedFilePath);

            db.UpdateShop(new ShopEntity
            {
                Id = db.get_currentShop().Id,
                Name = Username_txb.Text,
                Password = Password_pb.Password,
                Logo = fileBytes
            });

            ProfileUpdated?.Invoke();
        }
    }
}

