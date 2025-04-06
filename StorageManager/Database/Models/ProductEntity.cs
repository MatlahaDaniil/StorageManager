using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; }
        public float PurchasePrice { get; set; }
        public float Cost { get; set; }
        public byte[]? Image { get; set; }
        public CastomerEntity? Castomer  { get; set; }
        public HistoryEntity? History  { get; set; }
    }
}
