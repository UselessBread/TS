﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TA.Data;
using TA.Data.Contracts.Dto;

#nullable disable

namespace TA.Data.Migrations
{
    [DbContext(typeof(AssignedTestsContext))]
    partial class AssignedTestsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TA.Data.Contracts.Entities.AssignedTestReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AssignedTestImmutableId")
                        .HasColumnType("uuid");

                    b.Property<List<Comments>>("Comments")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<List<Correction>>("Corrections")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("TA.Data.Contracts.Entities.AssignedTests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AssignedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AssignedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DueTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("GroupImmutableId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ImmutableId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<Guid?>("StudentImmutableId")
                        .HasColumnType("uuid");

                    b.Property<int>("TestDescriptionId")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AssignedTests");
                });

            modelBuilder.Entity("TA.Data.Contracts.Entities.StudentAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<List<Answer>>("Answers")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid>("AssignedTestImmutableId")
                        .HasColumnType("uuid");

                    b.Property<int>("StudentAnswerState")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("StudentAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
