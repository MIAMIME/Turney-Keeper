﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Turney_Keeper.Data;

#nullable disable

namespace Turney_Keeper.Migrations
{
    [DbContext(typeof(Turney_KeeperContext))]
    [Migration("20240119214944_Turniej_Brackets2")]
    partial class Turniej_Brackets2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Turney_Keeper.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Turneys.Models.BracketMatch", b =>
                {
                    b.Property<int>("MatchNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchNumber"), 1L, 1);

                    b.Property<int?>("BracketRoundId")
                        .HasColumnType("int");

                    b.Property<int>("LoserId")
                        .HasColumnType("int");

                    b.Property<int>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("MatchNumber");

                    b.HasIndex("BracketRoundId");

                    b.ToTable("BracketMatch");
                });

            modelBuilder.Entity("Turneys.Models.BracketRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RoundNumber")
                        .HasColumnType("int");

                    b.Property<int>("TurneyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurneyId");

                    b.ToTable("BracketRound");
                });

            modelBuilder.Entity("Turneys.Models.Turney", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Admin_id")
                        .HasColumnType("int");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("Ending")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Opened")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Starting")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("UserIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Turneys");
                });

            modelBuilder.Entity("Turneys.Models.BracketMatch", b =>
                {
                    b.HasOne("Turneys.Models.BracketRound", null)
                        .WithMany("Matches")
                        .HasForeignKey("BracketRoundId");
                });

            modelBuilder.Entity("Turneys.Models.BracketRound", b =>
                {
                    b.HasOne("Turneys.Models.Turney", null)
                        .WithMany("BracketRounds")
                        .HasForeignKey("TurneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Turneys.Models.BracketRound", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("Turneys.Models.Turney", b =>
                {
                    b.Navigation("BracketRounds");
                });
#pragma warning restore 612, 618
        }
    }
}