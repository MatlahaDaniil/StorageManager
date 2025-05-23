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

        public void clear_currentShop() { currentShop = null; }
        public List<ProductEntity> getAllProducts()
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.Where(p => p.ShopId == currentShop.Id).ToList();
            }
        }

        public List<HistoryEntity> getAllHistories()
        {
            using (var context = new ShopDbContext())
            {
                return context.Histories.Where(h=> h.ShopId == currentShop.Id).ToList();
            }
        }

        public List<ProductEntity> get_Products(string productName)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.Where(p=> p.Name == productName).ToList();
            }
        }

        public CustomerEntity get_Customer(string phoneNumber)
        {
            using (var context = new ShopDbContext())
            {
                return context.Customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            }
        }

        public ProductEntity get_Product(Guid Id)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == Id);
            }
        }
        public CustomerEntity get_Customer(Guid Id)
        {
            using (var context = new ShopDbContext())
            {
                return context.Customers.FirstOrDefault(c => c.Id == Id);
            }
        }

        public bool DeleteProductById(Guid productId)
        {
            using (var context = new ShopDbContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void AddNewShop(ShopEntity newShop)
        {
            using (var context = new ShopDbContext())
            {
                context.Shops.Add(newShop);
                context.SaveChanges();
                currentShop = newShop;
            }
        }
        public void AddProduct(ProductEntity newProduct)
        {
            using (var context = new ShopDbContext())
            {
                context.Products.Add(newProduct);
                context.SaveChanges();
            }
        }

        public void AddCustomer(CustomerEntity newCustomer)
        {
            using (var context = new ShopDbContext())
            {
                context.Customers.Add(newCustomer);
                context.SaveChanges();
            }
        }

        public void AddHistory(HistoryEntity newHistory)
        {
            using (var context = new ShopDbContext())
            {
                context.Histories.Add(newHistory);
                context.SaveChanges();
            }
        }
        public bool CheckShop(ShopEntity checkShop)
        {
            using (var context = new ShopDbContext())
            {
                currentShop = context.Shops.FirstOrDefault(s => s.Name == checkShop.Name);
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

        public void UpdateProduct(ProductEntity updatedProduct)
        {
            using (var context = new ShopDbContext())
            {
                var product = context.Products.FirstOrDefault(l => l.Id == updatedProduct.Id);
                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Description = updatedProduct.Description;
                    product.Count = updatedProduct.Count;
                    product.PurchasePrice = updatedProduct.PurchasePrice;
                    product.Cost = updatedProduct.Cost;
                    product.Image = updatedProduct.Image;
                    product.ShopId = updatedProduct.ShopId;
                    context.SaveChanges();
                }
            }
        }
    }
}
