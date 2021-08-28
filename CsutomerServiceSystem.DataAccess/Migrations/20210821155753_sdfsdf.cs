using Microsoft.EntityFrameworkCore.Migrations;

namespace CsutomerServiceSystem.DataAccess.Migrations
{
    public partial class sdfsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordCode_User_UserId",
                table: "PasswordCode");

            migrationBuilder.DropIndex(
                name: "IX_PasswordCode_UserId",
                table: "PasswordCode");

            migrationBuilder.AddColumn<int>(
                name: "PasswordCodeId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordCodeId",
                table: "User",
                column: "PasswordCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_PasswordCode_PasswordCodeId",
                table: "User",
                column: "PasswordCodeId",
                principalTable: "PasswordCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PasswordCode_PasswordCodeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PasswordCodeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordCodeId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordCode_UserId",
                table: "PasswordCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordCode_User_UserId",
                table: "PasswordCode",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
