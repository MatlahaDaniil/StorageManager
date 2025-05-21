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
    internal class ShopConfiguration : IEntityTypeConfiguration<ShopEntity>
    {
        public void Configure(EntityTypeBuilder<ShopEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .HasMany(h => h.Histories)
                .WithOne(s => s.Shop);
        }
    }
}
