﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TS.Data;
using TS.Data.Contracts.DTO;

#nullable disable

namespace TS.Data.Migrations
{
    [DbContext(typeof(TestsContext))]
    partial class TestsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TS.Data.Contracts.Entities.TestDescriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ImmutableId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TestContentId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestContentImmutableId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TestDescriptions", (string)null);
                });

            modelBuilder.Entity("TS.Data.Contracts.Entities.TestsContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ImmutableId")
                        .HasColumnType("uuid");

                    b.Property<List<TaskDto>>("Tasks")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TestsContent", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
