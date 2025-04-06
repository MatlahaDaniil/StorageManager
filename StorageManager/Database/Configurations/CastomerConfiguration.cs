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
    public class CastomerConfiguration : IEntityTypeConfiguration<CastomerEntity>
    {
        public void Configure(EntityTypeBuilder<CastomerEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .HasMany(p => p.Products)
                .WithOne(c => c.Castomer);
        }
    }
}
