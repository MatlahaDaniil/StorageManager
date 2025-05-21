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
                .WithMany(h => h.Histories);

            builder
                .HasOne(c => c.Castomer)
                .WithMany(h => h.Histories);

            builder
                .HasOne(p => p.Product)
                .WithOne(h => h.History)
                .HasForeignKey<HistoryEntity>(p => p.ProductId);
        }
    }
}
