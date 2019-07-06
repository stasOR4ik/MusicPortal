﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MusicPortal.Repo;
using System;

namespace MusicPortal.Repo.Migrations
{
    [DbContext(typeof(MusicContext))]
    [Migration("20180625105116_Fix errors")]
    partial class Fixerrors
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArtistId");

                    b.Property<string>("Name");

                    b.Property<string>("PictureLink");

                    b.Property<string>("ShortBiography");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Core.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography");

                    b.Property<string>("Name");

                    b.Property<string>("PictureLink");

                    b.Property<string>("ShortBiography");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Core.ArtistSimilarArtist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArtistId");

                    b.Property<int?>("SimilarArtistId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SimilarArtistId");

                    b.ToTable("SimilarArtists");
                });

            modelBuilder.Entity("Core.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlbumId");

                    b.Property<int?>("ArtistId");

                    b.Property<string>("Duration");

                    b.Property<string>("Mp3Path");

                    b.Property<string>("Name");

                    b.Property<string>("PictureLink");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Core.Album", b =>
                {
                    b.HasOne("Core.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId");
                });

            modelBuilder.Entity("Core.ArtistSimilarArtist", b =>
                {
                    b.HasOne("Core.Artist", "Artist")
                        .WithMany("SimilarArtists")
                        .HasForeignKey("ArtistId");

                    b.HasOne("Core.Artist", "SimilarArtist")
                        .WithMany()
                        .HasForeignKey("SimilarArtistId");
                });

            modelBuilder.Entity("Core.Track", b =>
                {
                    b.HasOne("Core.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId");

                    b.HasOne("Core.Artist", "Artist")
                        .WithMany("Tracks")
                        .HasForeignKey("ArtistId");
                });
#pragma warning restore 612, 618
        }
    }
}