﻿// <auto-generated />
using System;
using Facebook.Archive.Data.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Facebook.Archive.Data.Migrations
{
    [DbContext(typeof(FacebookDbContext))]
    [Migration("20201121205151_AddPostsAndUpdates")]
    partial class AddPostsAndUpdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Facebook.Archive.Data.Model.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pages", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Posts", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.Update", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("EndUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Update", "facebook");
                });
#pragma warning restore 612, 618
        }
    }
}
