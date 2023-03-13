﻿// <auto-generated />
using System;
using InvoiceApp.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvoiceApp.Migrations
{
    [DbContext(typeof(InvoiceDataContext))]
    partial class InvoiceDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvoiceApp.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EuropeanUnion")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VATPrecent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Individual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Individuals");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BilledLegalPersonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PayedIndividualId")
                        .HasColumnType("int");

                    b.Property<int?>("PayedLegalPersonId")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BilledLegalPersonId");

                    b.HasIndex("PayedIndividualId");

                    b.HasIndex("PayedLegalPersonId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BasePrice")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("PriceWithVAT")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TotalItemPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.LegalPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("VATPayer")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("LegalPersons");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Individual", b =>
                {
                    b.HasOne("InvoiceApp.Models.Entities.Country", "Country")
                        .WithMany("Individuals")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Invoice", b =>
                {
                    b.HasOne("InvoiceApp.Models.Entities.LegalPerson", "BilledLegalPerson")
                        .WithMany()
                        .HasForeignKey("BilledLegalPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Entities.Individual", "PayedIndividual")
                        .WithMany()
                        .HasForeignKey("PayedIndividualId");

                    b.HasOne("InvoiceApp.Models.Entities.LegalPerson", "PayedLegalPerson")
                        .WithMany()
                        .HasForeignKey("PayedLegalPersonId");

                    b.Navigation("BilledLegalPerson");

                    b.Navigation("PayedIndividual");

                    b.Navigation("PayedLegalPerson");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.InvoiceItem", b =>
                {
                    b.HasOne("InvoiceApp.Models.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.LegalPerson", b =>
                {
                    b.HasOne("InvoiceApp.Models.Entities.Country", "Country")
                        .WithMany("LegalPersons")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Country", b =>
                {
                    b.Navigation("Individuals");

                    b.Navigation("LegalPersons");
                });

            modelBuilder.Entity("InvoiceApp.Models.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
