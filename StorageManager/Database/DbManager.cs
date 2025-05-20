using Database.SQL;
using Database.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManager.Database
{
    class DbManager
    {
        private static DbManager _instance;
        private ShopEntity currentShop = null;
        private DbManager()
        {
            var context = new ShopDbContext();
            context.Database.EnsureCreated();
        }
        public static DbManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                    {
                        _instance = new DbManager();
                    }
                }
                return _instance;
            }
        }

        public ShopEntity get_currentShop() { return currentShop; }

        public void AddNewShop(ShopEntity newShop)
        {
            using (var context = new ShopDbContext())
            {
                context.Shops.Add(newShop);
                context.SaveChanges();
                currentShop = newShop;
            }
        }

        public bool CheckShop(ShopEntity checkShop)
        {
            using (var context = new ShopDbContext())
            {
                currentShop = context.Shops
                    .FirstOrDefault(s => s.Name == checkShop.Name);
                return true;
            }
            return false;
        }

        public void UpdateShop(ShopEntity updatedShop)
        {
            using (var context = new ShopDbContext())
            {
                var shop = context.Shops.FirstOrDefault(l => l.Id == updatedShop.Id);
                if (shop != null)
                {
                    shop.Name = updatedShop.Name;
                    shop.Password = updatedShop.Password;
                    shop.Logo = updatedShop.Logo;

                    context.SaveChanges();
                    currentShop = updatedShop;
                }
            }
        }
    }
}
