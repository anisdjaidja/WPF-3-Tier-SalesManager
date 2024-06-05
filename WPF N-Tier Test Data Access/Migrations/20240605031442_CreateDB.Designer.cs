﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPF_N_Tier_Test_Data_Access.DataAccess;

#nullable disable

namespace WPF_N_Tier_Test_Data_Access.Migrations
{
    [DbContext(typeof(SalesContext))]
    [Migration("20240605031442_CreateDB")]
    partial class CreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_A")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BasePrice")
                        .HasColumnType("float");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("QuantityCap")
                        .HasColumnType("float");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.Transaction<WPF_N_Tier_Test_Data_Access.DTOs.TransactionBatch>", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShipmentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("discount")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.TransactionBatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("TransactionID")
                        .HasColumnType("int")
                        .HasColumnName("Transaction<TransactionBatch>ID");

                    b.Property<double>("UnitCost")
                        .HasColumnType("float");

                    b.Property<string>("UnitMetric")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.Property<double>("discount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TransactionID");

                    b.ToTable("TransactionBatch");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.TransactionBatch", b =>
                {
                    b.HasOne("WPF_N_Tier_Test_Data_Access.DTOs.Transaction<WPF_N_Tier_Test_Data_Access.DTOs.TransactionBatch>", null)
                        .WithMany("TransactedEntities")
                        .HasForeignKey("TransactionID");
                });

            modelBuilder.Entity("WPF_N_Tier_Test_Data_Access.DTOs.Transaction<WPF_N_Tier_Test_Data_Access.DTOs.TransactionBatch>", b =>
                {
                    b.Navigation("TransactedEntities");
                });
#pragma warning restore 612, 618
        }
    }
}