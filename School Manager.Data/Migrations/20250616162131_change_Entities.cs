using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class change_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_DriverContracts_DriverContractRef",
                table: "Cheques");

            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_ServiceContracts_ServiceContractRef",
                table: "Cheques");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_LocationData_AddressNavigationId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_AddressNavigationId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Cheques_DriverContractRef",
                table: "Cheques");

            migrationBuilder.DropIndex(
                name: "IX_Cheques_ServiceContractRef",
                table: "Cheques");

            migrationBuilder.DropColumn(
                name: "AddressNavigationId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "LocationDataRef",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DriverContractRef",
                table: "Cheques");

            migrationBuilder.DropColumn(
                name: "ServiceContractRef",
                table: "Cheques");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Schools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Schools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMale",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "LocationPairRef",
                table: "LocationData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Warnning",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DriverContractCheque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverContractRef = table.Column<long>(type: "bigint", nullable: false),
                    ChequeRef = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverContractCheque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverContractCheque_Cheques_DriverContractRef",
                        column: x => x.DriverContractRef,
                        principalTable: "Cheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverContractCheque_DriverContracts_DriverContractRef",
                        column: x => x.DriverContractRef,
                        principalTable: "DriverContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceContractCheque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceContractRef = table.Column<long>(type: "bigint", nullable: false),
                    ChequeRef = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractCheque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContractCheque_Cheques_ServiceContractRef",
                        column: x => x.ServiceContractRef,
                        principalTable: "Cheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContractCheque_ServiceContracts_ServiceContractRef",
                        column: x => x.ServiceContractRef,
                        principalTable: "ServiceContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverContractCheque_DriverContractRef",
                table: "DriverContractCheque",
                column: "DriverContractRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractCheque_ServiceContractRef",
                table: "ServiceContractCheque",
                column: "ServiceContractRef");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverContractCheque");

            migrationBuilder.DropTable(
                name: "ServiceContractCheque");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "IsMale",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Warnning",
                table: "Drivers");

            migrationBuilder.AddColumn<long>(
                name: "AddressNavigationId",
                table: "Schools",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LocationDataRef",
                table: "Schools",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LocationPairRef",
                table: "LocationData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DriverContractRef",
                table: "Cheques",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceContractRef",
                table: "Cheques",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AddressNavigationId",
                table: "Schools",
                column: "AddressNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_DriverContractRef",
                table: "Cheques",
                column: "DriverContractRef");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_ServiceContractRef",
                table: "Cheques",
                column: "ServiceContractRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_DriverContracts_DriverContractRef",
                table: "Cheques",
                column: "DriverContractRef",
                principalTable: "DriverContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_ServiceContracts_ServiceContractRef",
                table: "Cheques",
                column: "ServiceContractRef",
                principalTable: "ServiceContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_LocationData_AddressNavigationId",
                table: "Schools",
                column: "AddressNavigationId",
                principalTable: "LocationData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
