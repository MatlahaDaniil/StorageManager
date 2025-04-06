using Database.SQL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Configurations
{
    internal class HistoryConfiguration : IEntityTypeConfiguration<HistoryEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .HasOne(s => s.Shop)
                .WithOne(h => h.History)
                .HasForeignKey<HistoryEntity>(s => s.ShopId);

            builder
                .HasMany(p => p.Products)
                .WithOne(h => h.History);
        }
    }
}
