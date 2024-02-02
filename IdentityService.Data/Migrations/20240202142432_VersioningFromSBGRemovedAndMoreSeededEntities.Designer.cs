﻿// <auto-generated />
using System;
using IdentityService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IdentityService.Data.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20240202142432_VersioningFromSBGRemovedAndMoreSeededEntities")]
    partial class VersioningFromSBGRemovedAndMoreSeededEntities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IdentityService.Data.Contracts.Entities.Groups", b =>
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

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("IdentityService.Data.Contracts.Entities.StudentsByGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("GroupImmutableId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StuedntId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("StudentsByGroups");
                });

            modelBuilder.Entity("IdentityService.Data.Contracts.Entities.TsUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7317bb72-7732-48f5-a34f-6110d503578d"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6447f07c-a813-43ff-b4ff-2f9816bb6dee",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Defaut",
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "DEFAULTADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEPg1FEh2dHSoPjYZPPlot0t35aY5QMb2dMtjUJwtvqNapaJsFaflLuAui0n3Co/pPA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "87aa762b-3fd0-42e5-af79-499ccb1b2c88",
                            Surname = "Admin",
                            TwoFactorEnabled = false,
                            UserName = "DefaultAdmin"
                        },
                        new
                        {
                            Id = new Guid("2a6ee01c-e688-456b-a469-af63aeb0ce8e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ddaa4745-f870-4fc2-86a1-4d48f30c0398",
                            Email = "teach@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Defaut",
                            NormalizedEmail = "TEACH@GMAIL.COM",
                            NormalizedUserName = "DEFAULTTEACH",
                            PasswordHash = "AQAAAAIAAYagAAAAEJD5qpzUsy1vbdYhCBnKHpN+LIZdl5B678f2lgrZzE27DSVAMWccPCvUgArsotrGag==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e61469ef-eacb-45a1-aa03-25d7e76b6a06",
                            Surname = "Teacher",
                            TwoFactorEnabled = false,
                            UserName = "DefaultTeach"
                        },
                        new
                        {
                            Id = new Guid("27c53444-1dc6-4cff-b6af-5faf5a7c7722"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2134dd3b-c91f-4ad0-aaf4-929823a9c2d0",
                            Email = "stud@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Defaut",
                            NormalizedEmail = "TEACH@GMAIL.COM",
                            NormalizedUserName = "DEFAULTSTUDENT",
                            PasswordHash = "AQAAAAIAAYagAAAAELBgGE498cWqpt75KDzYOuI59GwJrUgIQOv/eamQR5RyQRUrHfRGV88DPYRVXvINmg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5af1c59c-9180-4de0-8c45-715c9b5f154e",
                            Surname = "Student",
                            TwoFactorEnabled = false,
                            UserName = "DefaultStudent"
                        },
                        new
                        {
                            Id = new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d54eb9c7-1fc2-4e21-b5ed-32b06468d389",
                            Email = "eh@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Erich",
                            NormalizedEmail = "EH@GMAIL.COM",
                            NormalizedUserName = "EH",
                            PasswordHash = "AQAAAAIAAYagAAAAEGbRCPQCH2BMbpvIqB1XFGXGO+NZknVMsASNvQNcfYmwyAmFBOLH5wJgNGbY0vYGtw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4fbc1efd-8850-49d7-80e1-d07dca500f13",
                            Surname = "Hartmann",
                            TwoFactorEnabled = false,
                            UserName = "eh"
                        },
                        new
                        {
                            Id = new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bb78a038-8dc7-4d68-8822-27e379cef4fb",
                            Email = "gigateach@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Gigachad",
                            NormalizedEmail = "GIGATEACH@GMAIL.COM",
                            NormalizedUserName = "GIGATEACH",
                            PasswordHash = "AQAAAAIAAYagAAAAEPAekgdxbeDYcwuvELyXacdPJ22+bWuRA6SE2bTv0C6TISaezCUJ7OayqKBD8xp0kQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "dd455990-573c-4970-9893-c471b5c80603",
                            Surname = "Teacher",
                            TwoFactorEnabled = false,
                            UserName = "gigateach"
                        },
                        new
                        {
                            Id = new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2f95201d-d5bb-4e42-8d32-a1f1aeb66625",
                            Email = "sigt@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Sigma",
                            NormalizedEmail = "SIGT@GMAIL.COM",
                            NormalizedUserName = "SIGT",
                            PasswordHash = "AQAAAAIAAYagAAAAEFc+8EBZYl1rwUle1qiO2BwYf/rVfUQ6mxXISSjx556YhOpVo+pGxlLtxi1aM0xYjQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a71cabf7-ce05-4f3d-9764-2a6b1b41405a",
                            Surname = "Teacher",
                            TwoFactorEnabled = false,
                            UserName = "sigt"
                        },
                        new
                        {
                            Id = new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cf08167c-e55c-4a0d-9a5f-0d0a966482a8",
                            Email = "ieov@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Ivan",
                            NormalizedEmail = "IEOV@GMAIL.COM",
                            NormalizedUserName = "IEOV",
                            PasswordHash = "AQAAAAIAAYagAAAAEDYWSZ3c9pba+0OUyvJKkmhYYeRtyMw0XtVkLB2tFNAewfzFNM3PuK6X4e0ixFvtvQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c6ba869e-96f6-4a44-a3c4-309b0e460620",
                            Surname = "Eblanov",
                            TwoFactorEnabled = false,
                            UserName = "ieov"
                        },
                        new
                        {
                            Id = new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ecf79488-58d9-4c5d-b5b5-d49c3848f853",
                            Email = "ac@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Anton",
                            NormalizedEmail = "AC@GMAIL.COM",
                            NormalizedUserName = "AC",
                            PasswordHash = "AQAAAAIAAYagAAAAEBcrqYgMWQF/K3c/hOOlFA/eNFP6egxEDH18pCvYuCXOW+YeVPH3ZmNNCcegVodwvw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b532d42c-4961-4d83-bfae-898f1778ad3b",
                            Surname = "Condom",
                            TwoFactorEnabled = false,
                            UserName = "ac"
                        },
                        new
                        {
                            Id = new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "169a439b-9694-4187-a78c-d25a51e52e63",
                            Email = "tn@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Temp",
                            NormalizedEmail = "TN@GMAIL.COM",
                            NormalizedUserName = "TN",
                            PasswordHash = "AQAAAAIAAYagAAAAEFMMoOL/cXooYMZnEti5QLfWLMaJdttRmel3pKNhrHXiYZPZ7ZaXIPE2alf4Ie+O0A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "10911780-af6e-4dcb-9d45-96ac829c6260",
                            Surname = "Name",
                            TwoFactorEnabled = false,
                            UserName = "tn"
                        },
                        new
                        {
                            Id = new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "13b1b82f-05a7-47f7-b783-f5e137a7653c",
                            Email = "frs@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Fourth",
                            NormalizedEmail = "FRS@GMAIL.COM",
                            NormalizedUserName = "FRS",
                            PasswordHash = "AQAAAAIAAYagAAAAEK/2ttTmshX99feREDAtLzxVHD/GuWmVAnUcLPETbPolfOtd0hEm73x30pnMOTaCcQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c4b7c395-f259-4316-ab91-61a2552493ec",
                            Surname = "Student",
                            TwoFactorEnabled = false,
                            UserName = "frs"
                        },
                        new
                        {
                            Id = new Guid("a96fd176-89d4-4c53-98a6-a9103723889b"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0a459966-bafe-4e5a-be99-c6051fd00fe2",
                            Email = "ffs@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Fifth",
                            NormalizedEmail = "FFS@GMAIL.COM",
                            NormalizedUserName = "FFS",
                            PasswordHash = "AQAAAAIAAYagAAAAEAr/R5TtOpBVXT0DLt1eQBMzHxZAxj3UAOABbth5ap5+SvxkroNrzWogKXMM9/kPqg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "96a50e7a-22e9-4da7-b7a4-80b41ea356fe",
                            Surname = "Sudent",
                            TwoFactorEnabled = false,
                            UserName = "ffs"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("02fe34e6-d974-439a-ad6b-032ddc1cdd47"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"),
                            Name = "Teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"),
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7317bb72-7732-48f5-a34f-6110d503578d"),
                            RoleId = new Guid("02fe34e6-d974-439a-ad6b-032ddc1cdd47")
                        },
                        new
                        {
                            UserId = new Guid("2a6ee01c-e688-456b-a469-af63aeb0ce8e"),
                            RoleId = new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22")
                        },
                        new
                        {
                            UserId = new Guid("27c53444-1dc6-4cff-b6af-5faf5a7c7722"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        },
                        new
                        {
                            UserId = new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1"),
                            RoleId = new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22")
                        },
                        new
                        {
                            UserId = new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a"),
                            RoleId = new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22")
                        },
                        new
                        {
                            UserId = new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931"),
                            RoleId = new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22")
                        },
                        new
                        {
                            UserId = new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        },
                        new
                        {
                            UserId = new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        },
                        new
                        {
                            UserId = new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        },
                        new
                        {
                            UserId = new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        },
                        new
                        {
                            UserId = new Guid("a96fd176-89d4-4c53-98a6-a9103723889b"),
                            RoleId = new Guid("3bae9791-3baf-4c97-9e69-af551e65f309")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("IdentityService.Data.Contracts.Entities.TsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("IdentityService.Data.Contracts.Entities.TsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityService.Data.Contracts.Entities.TsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("IdentityService.Data.Contracts.Entities.TsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}