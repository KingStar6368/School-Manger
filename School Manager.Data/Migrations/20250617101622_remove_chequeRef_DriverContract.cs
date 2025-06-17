using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class remove_chequeRef_DriverContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChequeRef",
                table: "DriverContracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChequeRef",
                table: "DriverContracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
