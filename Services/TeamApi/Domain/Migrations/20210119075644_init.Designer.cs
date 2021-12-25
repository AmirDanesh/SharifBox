﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamApi.Domain.Context;

namespace TeamApi.Domain.Migrations
{
    [DbContext(typeof(TeamDomainContext))]
    [Migration("20210119075644_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TeamApi.Domain.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManagerUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("TeamDetailsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamDetailsId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamApi.Domain.Models.TeamDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActivityField")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TeamDetails");
                });

            modelBuilder.Entity("TeamApi.Domain.Models.TeamUser", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TeamId", "UserId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("TeamApi.Domain.Models.Team", b =>
                {
                    b.HasOne("TeamApi.Domain.Models.TeamDetails", "TeamDetails")
                        .WithMany()
                        .HasForeignKey("TeamDetailsId");

                    b.Navigation("TeamDetails");
                });

            modelBuilder.Entity("TeamApi.Domain.Models.TeamUser", b =>
                {
                    b.HasOne("TeamApi.Domain.Models.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TeamApi.Domain.Models.Team", b =>
                {
                    b.Navigation("TeamUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
