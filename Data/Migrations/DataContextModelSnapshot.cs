﻿// <auto-generated />
using System;
using BudgettingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgettingApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("BudgettingApp.Entities.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfTransaction")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTransaction")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionType")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("TransactionHistories");
                });

            modelBuilder.Entity("BudgettingApp.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountStart")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("BudgettingApp.Entities.TransactionHistory", b =>
                {
                    b.HasOne("BudgettingApp.Entities.UserEntity", "UserEntity")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("BudgettingApp.Entities.UserEntity", b =>
                {
                    b.Navigation("TransactionHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
