using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Learner.Server.Migrations
{
    public partial class AlterTableDeveloperAddPositionIdAndCreateTablePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Developers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Developers_PositionId",
                table: "Developers",
                column: "PositionId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Position_PositionId",
                table: "Developers");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Developers_PositionId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Developers");
        }
    }
}
