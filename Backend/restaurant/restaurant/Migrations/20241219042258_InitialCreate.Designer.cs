﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using restrauntBooking;

#nullable disable

namespace restaurant.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241219042258_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("restrauntBooking.RestaurantUser", b =>
                {
                    b.Property<int>("userid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userid"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usertype")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userid");

                    b.ToTable("RestaurantUsers");
                });

            modelBuilder.Entity("restrauntBooking.Table", b =>
                {
                    b.Property<int>("tableid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tableid"));

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tablename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tableid");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("restrauntBooking.TableBooking", b =>
                {
                    b.Property<int>("bookingid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookingid"));

                    b.Property<string>("custname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tableid")
                        .HasColumnType("int");

                    b.HasKey("bookingid");

                    b.ToTable("TableBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
