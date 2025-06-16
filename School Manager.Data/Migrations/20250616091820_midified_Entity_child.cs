using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class midified_Entity_child : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_LocationPair_LocationPairRef",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_LocationPairRef",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "DriverRef",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "LocationPairRef",
                table: "Children");

            migrationBuilder.AddColumn<int>(
                name: "UserRef",
                table: "Parents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LocationPair",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserRef",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "SchoolRef",
                table: "Children",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserRef",
                table: "Parents",
                column: "UserRef");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPair_ChildRef",
                table: "LocationPair",
                column: "ChildRef");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserRef",
                table: "Drivers",
                column: "UserRef");

            migrationBuilder.CreateIndex(
                name: "IX_Children_SchoolRef",
                table: "Children",
                column: "SchoolRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Schools_SchoolRef",
                table: "Children",
                column: "SchoolRef",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_UserRef",
                table: "Drivers",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPair_Children_ChildRef",
                table: "LocationPair",
                column: "ChildRef",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Users_UserRef",
                table: "Parents",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Schools_SchoolRef",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_UserRef",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPair_Children_ChildRef",
                table: "LocationPair");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Users_UserRef",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserRef",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_LocationPair_ChildRef",
                table: "LocationPair");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_UserRef",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Children_SchoolRef",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LocationPair");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "SchoolRef",
                table: "Children");

            migrationBuilder.AddColumn<long>(
                name: "DriverRef",
                table: "Children",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LocationPairRef",
                table: "Children",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Children_LocationPairRef",
                table: "Children",
                column: "LocationPairRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_LocationPair_LocationPairRef",
                table: "Children",
                column: "LocationPairRef",
                principalTable: "LocationPair",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
