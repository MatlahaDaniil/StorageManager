using Database.SQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(i => i.Id);
            
            builder
                .HasOne(h => h.History)
                .WithOne(p => p.Product)
                .HasForeignKey<ProductEntity>(h => h.HistoryId);
        }
    }
}
