using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CsutomerServiceSystem.DataAccess.Migrations
{
    public partial class asdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Statu_StatuId",
                table: "Registration");

            migrationBuilder.AlterColumn<int>(
                name: "StatuId",
                table: "Registration",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PasswordCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordCode_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordCode_UserId",
                table: "PasswordCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Statu_StatuId",
                table: "Registration",
                column: "StatuId",
                principalTable: "Statu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Statu_StatuId",
                table: "Registration");

            migrationBuilder.DropTable(
                name: "PasswordCode");

            migrationBuilder.AlterColumn<int>(
                name: "StatuId",
                table: "Registration",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Statu_StatuId",
                table: "Registration",
                column: "StatuId",
                principalTable: "Statu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
