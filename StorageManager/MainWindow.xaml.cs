﻿using Database.SQL.Models;
using StorageManager.Database;
using StorageManager.Pages;
using StorageManager.Windows;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace StorageManager
{
    public partial class MainWindow : Window
    {
        ShopEntity currentShop = new ShopEntity();
        DbManager db = DbManager.Instance;
        void Start()
        {
            this.Visibility = Visibility.Hidden;

            StartWindow startWindow = new StartWindow();
            startWindow.ShowDialog();

            currentShop = db.get_currentShop();
            if (currentShop == null)
            {
                this.Close();
                return;
            }

            UpdateInfo();

            this.Visibility = Visibility.Visible;
        }
        public MainWindow()
        {
            InitializeComponent();

            closeImg.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "close.png")));
            expandImg.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "expand.png")));
            
            Start();
        }

        public void UpdateInfo()
        {
            currentShop = db.get_currentShop();
            ShopName_txb.Text = currentShop.Name;
            if (currentShop.Logo != null)
            {
                using (var stream = new MemoryStream(currentShop.Logo))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream; 
                    bitmap.EndInit();
                    bitmap.Freeze();

                    ShopLogo_img.Source = bitmap;
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ExpandWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MainBorder.CornerRadius = new CornerRadius(50);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StoragePage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StoragePage());
        }

        private void SalesHistoryPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HistorySalesPage());
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            EditProfilePage editProfilePage = new EditProfilePage();
            editProfilePage.ProfileUpdated += UpdateInfo;
            MainFrame.Navigate(editProfilePage);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            currentShop = null;
            db.clear_currentShop();
            MainFrame.Navigate(null);
            Start();
        }
    }
}
