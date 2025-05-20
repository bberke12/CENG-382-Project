using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CengProject.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorToFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_InstructorId",
                table: "Feedbacks",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_InstructorId",
                table: "Feedbacks",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_InstructorId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_InstructorId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
