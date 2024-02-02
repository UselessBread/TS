using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Data.Migrations
{
    /// <inheritdoc />
    public partial class VersioningFromSBGRemovedAndMoreSeededEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "StudentsByGroups");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "StudentsByGroups");

            migrationBuilder.DropColumn(
                name: "ImmutableId",
                table: "StudentsByGroups");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "StudentsByGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("27c53444-1dc6-4cff-b6af-5faf5a7c7722"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "2134dd3b-c91f-4ad0-aaf4-929823a9c2d0", "Defaut", "AQAAAAIAAYagAAAAELBgGE498cWqpt75KDzYOuI59GwJrUgIQOv/eamQR5RyQRUrHfRGV88DPYRVXvINmg==", "5af1c59c-9180-4de0-8c45-715c9b5f154e", "Student" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a6ee01c-e688-456b-a469-af63aeb0ce8e"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "ddaa4745-f870-4fc2-86a1-4d48f30c0398", "Defaut", "AQAAAAIAAYagAAAAEJD5qpzUsy1vbdYhCBnKHpN+LIZdl5B678f2lgrZzE27DSVAMWccPCvUgArsotrGag==", "e61469ef-eacb-45a1-aa03-25d7e76b6a06", "Teacher" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7317bb72-7732-48f5-a34f-6110d503578d"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "6447f07c-a813-43ff-b4ff-2f9816bb6dee", "Defaut", "AQAAAAIAAYagAAAAEPg1FEh2dHSoPjYZPPlot0t35aY5QMb2dMtjUJwtvqNapaJsFaflLuAui0n3Co/pPA==", "87aa762b-3fd0-42e5-af79-499ccb1b2c88", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584"), 0, "ecf79488-58d9-4c5d-b5b5-d49c3848f853", "ac@gmail.com", false, false, null, "Anton", "AC@GMAIL.COM", "AC", "AQAAAAIAAYagAAAAEBcrqYgMWQF/K3c/hOOlFA/eNFP6egxEDH18pCvYuCXOW+YeVPH3ZmNNCcegVodwvw==", null, false, "b532d42c-4961-4d83-bfae-898f1778ad3b", "Condom", false, "ac" },
                    { new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1"), 0, "d54eb9c7-1fc2-4e21-b5ed-32b06468d389", "eh@gmail.com", false, false, null, "Erich", "EH@GMAIL.COM", "EH", "AQAAAAIAAYagAAAAEGbRCPQCH2BMbpvIqB1XFGXGO+NZknVMsASNvQNcfYmwyAmFBOLH5wJgNGbY0vYGtw==", null, false, "4fbc1efd-8850-49d7-80e1-d07dca500f13", "Hartmann", false, "eh" },
                    { new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85"), 0, "13b1b82f-05a7-47f7-b783-f5e137a7653c", "frs@gmail.com", false, false, null, "Fourth", "FRS@GMAIL.COM", "FRS", "AQAAAAIAAYagAAAAEK/2ttTmshX99feREDAtLzxVHD/GuWmVAnUcLPETbPolfOtd0hEm73x30pnMOTaCcQ==", null, false, "c4b7c395-f259-4316-ab91-61a2552493ec", "Student", false, "frs" },
                    { new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8"), 0, "169a439b-9694-4187-a78c-d25a51e52e63", "tn@gmail.com", false, false, null, "Temp", "TN@GMAIL.COM", "TN", "AQAAAAIAAYagAAAAEFMMoOL/cXooYMZnEti5QLfWLMaJdttRmel3pKNhrHXiYZPZ7ZaXIPE2alf4Ie+O0A==", null, false, "10911780-af6e-4dcb-9d45-96ac829c6260", "Name", false, "tn" },
                    { new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a"), 0, "bb78a038-8dc7-4d68-8822-27e379cef4fb", "gigateach@gmail.com", false, false, null, "Gigachad", "GIGATEACH@GMAIL.COM", "GIGATEACH", "AQAAAAIAAYagAAAAEPAekgdxbeDYcwuvELyXacdPJ22+bWuRA6SE2bTv0C6TISaezCUJ7OayqKBD8xp0kQ==", null, false, "dd455990-573c-4970-9893-c471b5c80603", "Teacher", false, "gigateach" },
                    { new Guid("a96fd176-89d4-4c53-98a6-a9103723889b"), 0, "0a459966-bafe-4e5a-be99-c6051fd00fe2", "ffs@gmail.com", false, false, null, "Fifth", "FFS@GMAIL.COM", "FFS", "AQAAAAIAAYagAAAAEAr/R5TtOpBVXT0DLt1eQBMzHxZAxj3UAOABbth5ap5+SvxkroNrzWogKXMM9/kPqg==", null, false, "96a50e7a-22e9-4da7-b7a4-80b41ea356fe", "Sudent", false, "ffs" },
                    { new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931"), 0, "2f95201d-d5bb-4e42-8d32-a1f1aeb66625", "sigt@gmail.com", false, false, null, "Sigma", "SIGT@GMAIL.COM", "SIGT", "AQAAAAIAAYagAAAAEFc+8EBZYl1rwUle1qiO2BwYf/rVfUQ6mxXISSjx556YhOpVo+pGxlLtxi1aM0xYjQ==", null, false, "a71cabf7-ce05-4f3d-9764-2a6b1b41405a", "Teacher", false, "sigt" },
                    { new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df"), 0, "cf08167c-e55c-4a0d-9a5f-0d0a966482a8", "ieov@gmail.com", false, false, null, "Ivan", "IEOV@GMAIL.COM", "IEOV", "AQAAAAIAAYagAAAAEDYWSZ3c9pba+0OUyvJKkmhYYeRtyMw0XtVkLB2tFNAewfzFNM3PuK6X4e0ixFvtvQ==", null, false, "c6ba869e-96f6-4a44-a3c4-309b0e460620", "Eblanov", false, "ieov" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584") },
                    { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1") },
                    { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85") },
                    { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8") },
                    { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a") },
                    { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("a96fd176-89d4-4c53-98a6-a9103723889b") },
                    { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931") },
                    { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("a96fd176-89d4-4c53-98a6-a9103723889b") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5c24f991-cbc7-43c8-bbc6-f51ab6dfbd22"), new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3bae9791-3baf-4c97-9e69-af551e65f309"), new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1c1fd164-c4b2-4e96-9b4a-625eb6454584"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5a0d3404-1038-474c-a5bd-ba2177499bc1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7158dedd-a907-4d88-9dcd-1683464e4f85"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("882339c7-1d54-4ea5-b6f0-d247a21179d8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8b6c3b04-c5cc-41e1-bdef-87f459780d7a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a96fd176-89d4-4c53-98a6-a9103723889b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1cd61dd-29f1-4904-bebf-fc5fe1ffc931"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fde274ee-0a4a-4b53-93af-68fedcfc60df"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "StudentsByGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "StudentsByGroups",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImmutableId",
                table: "StudentsByGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "StudentsByGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("27c53444-1dc6-4cff-b6af-5faf5a7c7722"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "a279ad71-a388-4cd1-b2dd-00426a09c2b8", null, "AQAAAAIAAYagAAAAEDrFeSvMg66XgJz9lWlA+ii7lvBEYaaWAYWsPnQ3lovkyQWGUUCadMV4U7bU/n1aow==", "d812b7d0-3777-4ce1-9248-025f066ac66c", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a6ee01c-e688-456b-a469-af63aeb0ce8e"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "5a526ce3-9de3-4603-a680-618589f8fd16", null, "AQAAAAIAAYagAAAAEGif73hjGfkN9qtbsFskgVl1Ha1rqd3ER30B36iUyzkojdzBINUMoHjGWIG+55ZAUQ==", "b7ad4221-cd09-4b48-af93-79454e139d19", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7317bb72-7732-48f5-a34f-6110d503578d"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "9d5d7813-64e9-4194-a2af-5015e10d4a91", null, "AQAAAAIAAYagAAAAEGHNia1HSrtDvwCEPHNNMM9iJh79TPvoswAKYuOXivB5XMmMfR6cws+gjEsnsPC3QQ==", "ee4dbb28-1332-4823-b2fd-4e0d010d8bc6", null });
        }
    }
}
