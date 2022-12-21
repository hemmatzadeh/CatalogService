﻿// <auto-generated />
using FH.CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FH.CatalogService.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221217214647_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.AttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<string>("ValueDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ValueName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.ToTable("AttributeValues");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductAttributeValues", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("AttributeValueId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "AttributeValueId");

                    b.HasIndex("AttributeValueId");

                    b.ToTable("ProductAttributeValues");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductCategoryAttribute", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "AttributeId");

                    b.HasIndex("AttributeId");

                    b.ToTable("CategoryAttributes");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.AttributeValue", b =>
                {
                    b.HasOne("FH.CatalogService.Domain.Entities.ProductAttribute", "ProductAttribute")
                        .WithMany("AttributeValues")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductAttribute");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.Product", b =>
                {
                    b.HasOne("FH.CatalogService.Domain.Entities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductAttributeValues", b =>
                {
                    b.HasOne("FH.CatalogService.Domain.Entities.AttributeValue", "AttributeValue")
                        .WithMany("ProductAttributeValues")
                        .HasForeignKey("AttributeValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FH.CatalogService.Domain.Entities.Product", "Product")
                        .WithMany("ProductAttributeValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttributeValue");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductCategoryAttribute", b =>
                {
                    b.HasOne("FH.CatalogService.Domain.Entities.ProductAttribute", "ProductAttribute")
                        .WithMany("ProductCategoryAttributes")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FH.CatalogService.Domain.Entities.ProductCategory", "ProductCategory")
                        .WithMany("ProductCategoryAttributes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductAttribute");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.AttributeValue", b =>
                {
                    b.Navigation("ProductAttributeValues");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductAttributeValues");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductAttribute", b =>
                {
                    b.Navigation("AttributeValues");

                    b.Navigation("ProductCategoryAttributes");
                });

            modelBuilder.Entity("FH.CatalogService.Domain.Entities.ProductCategory", b =>
                {
                    b.Navigation("ProductCategoryAttributes");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
