using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class changelenghtofnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(128)",
                nullable: false,
                comment: "نام خانوادگی",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldComment: "نام خانوادگی");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(128)",
                nullable: false,
                comment: "نام",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldComment: "نام");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(1024)",
                nullable: false,
                comment: "نام مدرسه",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldComment: "نام مدرسه");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cars",
                type: "nvarchar(512)",
                nullable: false,
                comment: "نام ماشین",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldComment: "نام ماشین");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bills",
                type: "nvarchar(512)",
                nullable: false,
                comment: "نام قبض",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldComment: "نام قبض");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(50)",
                nullable: false,
                comment: "نام خانوادگی",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldComment: "نام خانوادگی");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(50)",
                nullable: false,
                comment: "نام",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldComment: "نام");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(128)",
                nullable: false,
                comment: "نام مدرسه",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldComment: "نام مدرسه");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cars",
                type: "nvarchar(50)",
                nullable: false,
                comment: "نام ماشین",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldComment: "نام ماشین");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bills",
                type: "nvarchar(50)",
                nullable: false,
                comment: "نام قبض",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldComment: "نام قبض");
        }
    }
}
