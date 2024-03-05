using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResuMeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSecurityQuestionsAndAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer3",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion3",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityAnswer1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityAnswer2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityAnswer3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion3",
                table: "AspNetUsers");
        }
    }
}
