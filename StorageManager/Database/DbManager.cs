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

        public void AddNewShop(ShopEntity newShop)
        {
            using (var context = new ShopDbContext())
            {
                context.Shops.Add(newShop);
                context.SaveChanges();
            }
        }

        public ShopEntity CheckShop(ShopEntity checkShop)
        {
            using (var context = new ShopDbContext())
            {
                return context.Shops
                    .FirstOrDefault(s => s.Name == checkShop.Name);
            }
        }
    }
}
