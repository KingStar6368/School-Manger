using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBillsBillsBehevioreResited_Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_ServiceContracts_ServiceContractRef",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_ServiceContracts_ServiceContractRef",
                table: "Bills",
                column: "ServiceContractRef",
                principalTable: "ServiceContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_ServiceContracts_ServiceContractRef",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_ServiceContracts_ServiceContractRef",
                table: "Bills",
                column: "ServiceContractRef",
                principalTable: "ServiceContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
