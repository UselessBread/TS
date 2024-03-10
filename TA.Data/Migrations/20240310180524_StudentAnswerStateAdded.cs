using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TA.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentAnswerStateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentAnswerState",
                table: "StudentAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentAnswerState",
                table: "StudentAnswers");
        }
    }
}
