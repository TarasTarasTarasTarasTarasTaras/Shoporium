﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shoporium.Data._EntityFramework;

#nullable disable

namespace Shoporium.Data.Migrations
{
    [DbContext(typeof(ShoporiumContext))]
    [Migration("20240120110441_AddedProducts")]
    partial class AddedProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMobileVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.LoginDetail", b =>
                {
                    b.Property<long>("LoginDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LoginDetailId"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastLoginAttemptUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginDetailId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("LoginDetails");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfViews")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ExpirationTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Token", b =>
                {
                    b.Property<long>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TokenId"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("TokenValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenId");

                    b.HasIndex("AccountId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.LoginDetail", b =>
                {
                    b.HasOne("Shoporium.Data._EntityFramework.Entities.Account", "Account")
                        .WithOne("LoginDetail")
                        .HasForeignKey("Shoporium.Data._EntityFramework.Entities.LoginDetail", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Product", b =>
                {
                    b.HasOne("Shoporium.Data._EntityFramework.Entities.Account", "Account")
                        .WithMany("Products")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoporium.Data._EntityFramework.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.ProductCategory", b =>
                {
                    b.HasOne("Shoporium.Data._EntityFramework.Entities.ProductCategory", "MainCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("MainCategoryId");

                    b.Navigation("MainCategory");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.RefreshToken", b =>
                {
                    b.HasOne("Shoporium.Data._EntityFramework.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Token", b =>
                {
                    b.HasOne("Shoporium.Data._EntityFramework.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.Account", b =>
                {
                    b.Navigation("LoginDetail")
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shoporium.Data._EntityFramework.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Subcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
