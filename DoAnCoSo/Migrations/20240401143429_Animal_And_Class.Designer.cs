﻿// <auto-generated />
using System;
using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoAnCoSo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240401143429_Animal_And_Class")]
    partial class Animal_And_Class
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DoAnCoSo.Models.Animal", b =>
                {
                    b.Property<int>("IdAnimal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAnimal"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioiThieuText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdClassAnimal")
                        .HasColumnType("int");

                    b.Property<string>("ImgQR3D")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NgoaiHinhText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiSinhSongImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiSinhSongSongText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAnimal");

                    b.HasIndex("IdClassAnimal");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("DoAnCoSo.Models.AnimalImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimalsIdAnimal")
                        .HasColumnType("int");

                    b.Property<int>("IdAnimal")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalsIdAnimal");

                    b.ToTable("AnimalImage");
                });

            modelBuilder.Entity("DoAnCoSo.Models.ClassAnimal", b =>
                {
                    b.Property<int>("IdClass")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClass"));

                    b.Property<string>("BackgroundVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClass");

                    b.ToTable("ClassAnimals");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Comment", b =>
                {
                    b.Property<int>("IdCmt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCmt"));

                    b.Property<string>("ChatData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPost")
                        .HasColumnType("int");

                    b.HasKey("IdCmt");

                    b.HasIndex("IdPost");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Post", b =>
                {
                    b.Property<int>("IdPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPost"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageQRVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPost");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Animal", b =>
                {
                    b.HasOne("DoAnCoSo.Models.ClassAnimal", "ClassAnimal")
                        .WithMany("Animals")
                        .HasForeignKey("IdClassAnimal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassAnimal");
                });

            modelBuilder.Entity("DoAnCoSo.Models.AnimalImage", b =>
                {
                    b.HasOne("DoAnCoSo.Models.Animal", "Animals")
                        .WithMany("ListImage")
                        .HasForeignKey("AnimalsIdAnimal");

                    b.Navigation("Animals");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Comment", b =>
                {
                    b.HasOne("DoAnCoSo.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("IdPost")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Animal", b =>
                {
                    b.Navigation("ListImage");
                });

            modelBuilder.Entity("DoAnCoSo.Models.ClassAnimal", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("DoAnCoSo.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
