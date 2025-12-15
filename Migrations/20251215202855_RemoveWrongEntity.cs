using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigSonar.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWrongEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_Subgenre_ArtistGenreId",
                table: "Artist");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Subgenre_GenreId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Subgenre_Subgenre_SubGenreId",
                table: "Subgenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subgenre",
                table: "Subgenre");

            migrationBuilder.RenameTable(
                name: "Subgenre",
                newName: "Genre");

            migrationBuilder.RenameIndex(
                name: "IX_Subgenre_SubGenreId",
                table: "Genre",
                newName: "IX_Genre_SubGenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_Genre_ArtistGenreId",
                table: "Artist",
                column: "ArtistGenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Genre_GenreId",
                table: "Event",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Artist_Genre_ArtistGenreId",
                table: "Artist");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Genre_GenreId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Genre_SubGenreId",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Subgenre");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_SubGenreId",
                table: "Subgenre",
                newName: "IX_Subgenre_SubGenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subgenre",
                table: "Subgenre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_Subgenre_ArtistGenreId",
                table: "Artist",
                column: "ArtistGenreId",
                principalTable: "Subgenre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Subgenre_GenreId",
                table: "Event",
                column: "GenreId",
                principalTable: "Subgenre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subgenre_Subgenre_SubGenreId",
                table: "Subgenre",
                column: "SubGenreId",
                principalTable: "Subgenre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
