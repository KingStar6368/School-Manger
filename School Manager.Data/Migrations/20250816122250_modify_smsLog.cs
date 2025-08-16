using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class modify_smsLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSLogs_Users_UserRef",
                table: "SMSLogs");

            migrationBuilder.DropIndex(
                name: "IX_SMSLogs_UserRef",
                table: "SMSLogs");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "SMSLogs");

            migrationBuilder.CreateIndex(
                name: "IX_SMSLogs_UserId",
                table: "SMSLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSLogs_Users_UserId",
                table: "SMSLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSLogs_Users_UserId",
                table: "SMSLogs");

            migrationBuilder.DropIndex(
                name: "IX_SMSLogs_UserId",
                table: "SMSLogs");

            migrationBuilder.AddColumn<long>(
                name: "UserRef",
                table: "SMSLogs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SMSLogs_UserRef",
                table: "SMSLogs",
                column: "UserRef");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSLogs_Users_UserRef",
                table: "SMSLogs",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
