using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigSonar.Migrations
{
    /// <inheritdoc />
    public partial class MakeSubGenreOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Genre_SubGenreId",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "SubGenreId",
                table: "Genre",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Genre_SubGenreId",
                table: "Genre",
                column: "SubGenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Genre_SubGenreId",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "SubGenreId",
                table: "Genre",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Genre_SubGenreId",
                table: "Genre",
                column: "SubGenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
