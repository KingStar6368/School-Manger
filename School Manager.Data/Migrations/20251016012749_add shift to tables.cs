using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addshifttotables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChildren_Drivers_DriverRef",
                table: "DriverChildren");

            migrationBuilder.DropIndex(
                name: "IX_DriverChildren_DriverRef",
                table: "DriverChildren");

            migrationBuilder.DropColumn(
                name: "DriverRef",
                table: "DriverChildren");

            migrationBuilder.AddColumn<long>(
                name: "DriverId",
                table: "DriverChildren",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DriverShiftRef",
                table: "DriverChildren",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolRef = table.Column<long>(type: "bigint", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    ShiftTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time", nullable: false),
                    End = table.Column<TimeOnly>(type: "time", nullable: false),
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
                    table.PrimaryKey("PK_shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shifts_Schools_SchoolRef",
                        column: x => x.SchoolRef,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverShifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftRef = table.Column<long>(type: "bigint", nullable: false),
                    DriverRef = table.Column<long>(type: "bigint", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DriverShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverShifts_Drivers_DriverRef",
                        column: x => x.DriverRef,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverShifts_shifts_ShiftRef",
                        column: x => x.ShiftRef,
                        principalTable: "shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverChildren_DriverId",
                table: "DriverChildren",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChildren_DriverShiftRef",
                table: "DriverChildren",
                column: "DriverShiftRef");

            migrationBuilder.CreateIndex(
                name: "IX_DriverShifts_DriverRef",
                table: "DriverShifts",
                column: "DriverRef");

            migrationBuilder.CreateIndex(
                name: "IX_DriverShifts_ShiftRef",
                table: "DriverShifts",
                column: "ShiftRef");

            migrationBuilder.CreateIndex(
                name: "IX_shifts_SchoolRef",
                table: "shifts",
                column: "SchoolRef");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChildren_DriverShifts_DriverShiftRef",
                table: "DriverChildren",
                column: "DriverShiftRef",
                principalTable: "DriverShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChildren_Drivers_DriverId",
                table: "DriverChildren",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChildren_DriverShifts_DriverShiftRef",
                table: "DriverChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChildren_Drivers_DriverId",
                table: "DriverChildren");

            migrationBuilder.DropTable(
                name: "DriverShifts");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropIndex(
                name: "IX_DriverChildren_DriverId",
                table: "DriverChildren");

            migrationBuilder.DropIndex(
                name: "IX_DriverChildren_DriverShiftRef",
                table: "DriverChildren");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DriverChildren");

            migrationBuilder.DropColumn(
                name: "DriverShiftRef",
                table: "DriverChildren");

            migrationBuilder.AddColumn<long>(
                name: "DriverRef",
                table: "DriverChildren",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChildren_DriverRef",
                table: "DriverChildren",
                column: "DriverRef");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChildren_Drivers_DriverRef",
                table: "DriverChildren",
                column: "DriverRef",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
