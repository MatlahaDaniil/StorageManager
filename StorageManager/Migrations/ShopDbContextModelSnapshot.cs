﻿// <auto-generated />
using System;
using Database.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StorageManager.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.SQL.Models.CastomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Castomers");
                });

            modelBuilder.Entity("Database.SQL.Models.HistoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShopId")
                        .IsUnique();

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Database.SQL.Models.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CastomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PurchasePrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CastomerId");

                    b.HasIndex("HistoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Database.SQL.Models.ShopEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Database.SQL.Models.HistoryEntity", b =>
                {
                    b.HasOne("Database.SQL.Models.ShopEntity", "Shop")
                        .WithOne("History")
                        .HasForeignKey("Database.SQL.Models.HistoryEntity", "ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Database.SQL.Models.ProductEntity", b =>
                {
                    b.HasOne("Database.SQL.Models.CastomerEntity", "Castomer")
                        .WithMany("Products")
                        .HasForeignKey("CastomerId");

                    b.HasOne("Database.SQL.Models.HistoryEntity", "History")
                        .WithMany("Products")
                        .HasForeignKey("HistoryId");

                    b.Navigation("Castomer");

                    b.Navigation("History");
                });

            modelBuilder.Entity("Database.SQL.Models.CastomerEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Database.SQL.Models.HistoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Database.SQL.Models.ShopEntity", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
