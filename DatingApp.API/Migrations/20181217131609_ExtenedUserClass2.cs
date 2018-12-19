using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtenedUserClass2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Users_UserId",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Photo",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                newName: "IX_Photo_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Photo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Users_UserID",
                table: "Photo",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Users_UserID",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Photo",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_UserID",
                table: "Photo",
                newName: "IX_Photo_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Users_UserId",
                table: "Photo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
