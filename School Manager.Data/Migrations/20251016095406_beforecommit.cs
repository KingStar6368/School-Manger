using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class beforecommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "shifts",
                type: "nvarchar(50)",
                nullable: true,
                comment: "نام شیفت");

            migrationBuilder.AddColumn<long>(
                name: "ShiftId",
                table: "Children",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Children_ShiftId",
                table: "Children",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_shifts_ShiftId",
                table: "Children",
                column: "ShiftId",
                principalTable: "shifts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_shifts_ShiftId",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_ShiftId",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "shifts");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Children");
        }
    }
}
