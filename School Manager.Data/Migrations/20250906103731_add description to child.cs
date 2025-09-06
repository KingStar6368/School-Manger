using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddescriptiontochild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceContracts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Children",
                type: "NVARCHAR(MAX)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Children");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceContracts",
                type: "NVARCHAR(MAX)",
                nullable: true);
        }
    }
}
