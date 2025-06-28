using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class trackinCode_Is_Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "Pays",
                type: "nvarchar(32)",
                nullable: true,
                comment: "کد رهگیری",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "Pays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldNullable: true,
                oldComment: "کد رهگیری");
        }
    }
}
