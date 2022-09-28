﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsStore.Model;

#nullable disable

namespace SportsStore.Model.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20220921220530_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SportsStore.Model.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemInnerType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Ball", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.HasDiscriminator().HasValue("Ball");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Bat", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.HasDiscriminator().HasValue("Bat");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Net", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.HasDiscriminator().HasValue("Net");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Pant", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.Property<string>("Size")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Pant");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Shirt", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.Property<string>("Size")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Shirt");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Shoe", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.Property<string>("Size")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Shoe");
                });

            modelBuilder.Entity("SportsStore.Model.Items.Short", b =>
                {
                    b.HasBaseType("SportsStore.Model.Items.Item");

                    b.Property<string>("Size")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Short");
                });
#pragma warning restore 612, 618
        }
    }
}
