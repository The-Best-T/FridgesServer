﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Server.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Fridge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FridgeId");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("OwnerName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Fridges");

                    b.HasData(
                        new
                        {
                            Id = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                            ModelId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Name = "Fridge1",
                            OwnerName = "Boston Griffin"
                        },
                        new
                        {
                            Id = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                            ModelId = new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"),
                            Name = "Fridge2",
                            OwnerName = "Silas Evans"
                        },
                        new
                        {
                            Id = new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"),
                            ModelId = new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f"),
                            Name = "Fridge3",
                            OwnerName = "Seth Hughes"
                        },
                        new
                        {
                            Id = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                            ModelId = new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"),
                            Name = "Fridge4",
                            OwnerName = "Gary Bryant"
                        });
                });

            modelBuilder.Entity("Entities.Models.FridgeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FridgeModelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FridgeModels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Name = "Beko RCSK 310M20",
                            Year = 2018
                        },
                        new
                        {
                            Id = new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"),
                            Name = "Tesler RC-55 White",
                            Year = 2019
                        },
                        new
                        {
                            Id = new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f"),
                            Name = "Pozis RK-139 W",
                            Year = 2020
                        });
                });

            modelBuilder.Entity("Entities.Models.FridgeProduct", b =>
                {
                    b.Property<Guid>("FridgeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("FridgeId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("FridgeProduct");

                    b.HasData(
                        new
                        {
                            FridgeId = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                            ProductId = new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"),
                            Quantity = 2
                        },
                        new
                        {
                            FridgeId = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                            ProductId = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                            Quantity = 1
                        },
                        new
                        {
                            FridgeId = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                            ProductId = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                            Quantity = 6
                        },
                        new
                        {
                            FridgeId = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                            ProductId = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                            Quantity = 3
                        },
                        new
                        {
                            FridgeId = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                            ProductId = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                            Quantity = 1
                        },
                        new
                        {
                            FridgeId = new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"),
                            ProductId = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                            Quantity = 2
                        },
                        new
                        {
                            FridgeId = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                            ProductId = new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"),
                            Quantity = 3
                        },
                        new
                        {
                            FridgeId = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                            ProductId = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<int>("DefaulQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"),
                            DefaulQuantity = 2,
                            Name = "Tomato"
                        },
                        new
                        {
                            Id = new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"),
                            DefaulQuantity = 1,
                            Name = "Lemon"
                        },
                        new
                        {
                            Id = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                            DefaulQuantity = 1,
                            Name = "Milk"
                        },
                        new
                        {
                            Id = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                            DefaulQuantity = 5,
                            Name = "Potato"
                        },
                        new
                        {
                            Id = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                            DefaulQuantity = 2,
                            Name = "Onion"
                        });
                });

            modelBuilder.Entity("Entities.Models.Fridge", b =>
                {
                    b.HasOne("Entities.Models.FridgeModel", "Model")
                        .WithMany("Fridges")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Entities.Models.FridgeProduct", b =>
                {
                    b.HasOne("Entities.Models.Fridge", "Fridge")
                        .WithMany("FridgeProducts")
                        .HasForeignKey("FridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Product", "Product")
                        .WithMany("FridgeProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fridge");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Models.Fridge", b =>
                {
                    b.Navigation("FridgeProducts");
                });

            modelBuilder.Entity("Entities.Models.FridgeModel", b =>
                {
                    b.Navigation("Fridges");
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.Navigation("FridgeProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
