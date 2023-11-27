﻿// <auto-generated />
using System;
using Eventmi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eventmi.Infrastructure.Migrations
{
    [DbContext(typeof(EventmiDbContext))]
    [Migration("20231127151736_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Eventmi.Infrastructure.Data.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Идентификатор на адреса");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)")
                        .HasComment("Улица");

                    b.Property<int>("TownId")
                        .HasColumnType("int")
                        .HasComment("Идентификатор на град");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("Address");

                    b.HasComment("Място на провеждане");
                });

            modelBuilder.Entity("Eventmi.Infrastructure.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Идентификатор на събитието");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2")
                        .HasComment("Дата на изтриване");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2")
                        .HasComment("Край на събитието");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Активност на събитието");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Име на събитието");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2")
                        .HasComment("Начало на събитието");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Event");

                    b.HasComment("Събитие");
                });

            modelBuilder.Entity("Eventmi.Infrastructure.Data.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Идентификатор на град");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Име на град");

                    b.HasKey("Id");

                    b.ToTable("Town");

                    b.HasComment("Град");
                });

            modelBuilder.Entity("Eventmi.Infrastructure.Data.Models.Address", b =>
                {
                    b.HasOne("Eventmi.Infrastructure.Data.Models.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Town");
                });

            modelBuilder.Entity("Eventmi.Infrastructure.Data.Models.Event", b =>
                {
                    b.HasOne("Eventmi.Infrastructure.Data.Models.Address", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });
#pragma warning restore 612, 618
        }
    }
}