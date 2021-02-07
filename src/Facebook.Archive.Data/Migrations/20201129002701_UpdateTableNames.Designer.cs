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
    [Migration("20201129002701_UpdateTableNames")]
    partial class UpdateTableNames
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

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Html")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParserVersion")
                        .HasColumnType("int");

                    b.Property<int?>("PostUpdateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostUpdateId");

                    b.ToTable("PostContents", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrlHtml")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostContentId");

                    b.ToTable("PostContentImages", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PostContentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHtml")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostContentId");

                    b.ToTable("PostContentLinks", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Html")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostContentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostContentId");

                    b.ToTable("PostContentTexts", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentTimestamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PostContentId")
                        .HasColumnType("int");

                    b.Property<string>("TimestampRaw")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimestampUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PostContentId");

                    b.ToTable("PostContentTimestamps", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UpdateId");

                    b.ToTable("PostUpdates", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.Update", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("EndUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Updates", "facebook");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContent", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.PostUpdate", "PostUpdate")
                        .WithMany()
                        .HasForeignKey("PostUpdateId");

                    b.Navigation("PostUpdate");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentImage", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.PostContent", "PostContent")
                        .WithMany("Images")
                        .HasForeignKey("PostContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostContent");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentLink", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.PostContent", "PostContent")
                        .WithMany("Links")
                        .HasForeignKey("PostContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostContent");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentText", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.PostContent", "PostContent")
                        .WithMany("Texts")
                        .HasForeignKey("PostContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostContent");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContentTimestamp", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.PostContent", "PostContent")
                        .WithMany("Timestamps")
                        .HasForeignKey("PostContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostContent");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostUpdate", b =>
                {
                    b.HasOne("Facebook.Archive.Data.Model.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.HasOne("Facebook.Archive.Data.Model.Update", "Update")
                        .WithMany()
                        .HasForeignKey("UpdateId");

                    b.Navigation("Post");

                    b.Navigation("Update");
                });

            modelBuilder.Entity("Facebook.Archive.Data.Model.PostContent", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Links");

                    b.Navigation("Texts");

                    b.Navigation("Timestamps");
                });
#pragma warning restore 612, 618
        }
    }
}