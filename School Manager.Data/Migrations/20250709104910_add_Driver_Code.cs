using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_Driver_Code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Drivers",
                type: "nvarchar(50)",
                nullable: true,
                comment: "کد پرونده");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Drivers");
        }
    }
}
