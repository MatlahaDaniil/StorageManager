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
        public List<ProductEntity> Products { get; set; } = [];
        public ShopEntity? Shop { get; set; }
        public Guid ShopId { get; set; }
    }
}
