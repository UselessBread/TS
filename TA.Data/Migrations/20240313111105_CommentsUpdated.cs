using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using TA.Data.Contracts.Dto;

#nullable disable

namespace TA.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corrections",
                table: "Review");

            migrationBuilder.AddColumn<string>(
                name: "FinalComment",
                table: "Review",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentAnswerId",
                table: "Review",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalComment",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "StudentAnswerId",
                table: "Review");

            migrationBuilder.AddColumn<List<Correction>>(
                name: "Corrections",
                table: "Review",
                type: "jsonb",
                nullable: false);
        }
    }
}
