using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CengProject.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_InstructorId",
                table: "Reservations",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_InstructorId",
                table: "Reservations",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_InstructorId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_InstructorId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
