using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixCenterSportForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterSports_Centers_SportId",
                table: "CenterSports");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterSports_Sports_CenterId",
                table: "CenterSports");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterSports_Centers_CenterId",
                table: "CenterSports",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterSports_Sports_SportId",
                table: "CenterSports",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterSports_Centers_CenterId",
                table: "CenterSports");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterSports_Sports_SportId",
                table: "CenterSports");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterSports_Centers_SportId",
                table: "CenterSports",
                column: "SportId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterSports_Sports_CenterId",
                table: "CenterSports",
                column: "CenterId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
