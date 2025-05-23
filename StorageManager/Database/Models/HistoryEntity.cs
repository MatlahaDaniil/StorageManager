using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Models
{
    public class HistoryEntity
    {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }

        public ShopEntity Shop { get; set; } = null!;
        public CustomerEntity Customer { get; set; } = null!;
        public ProductEntity Product { get; set; } = null!;
    }
}
