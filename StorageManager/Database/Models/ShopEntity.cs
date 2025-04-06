using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Models
{
    public class ShopEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public HistoryEntity? History { get; set; }

        public Guid HistoryId { get; set; }
    }
}
