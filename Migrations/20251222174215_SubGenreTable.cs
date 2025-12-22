using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigSonar.Migrations
{
    /// <inheritdoc />
    public partial class SubGenreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genres_ArtistGenreId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "ArtistGenreId",
                table: "Artists",
                newName: "subGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Artists_ArtistGenreId",
                table: "Artists",
                newName: "IX_Artists_subGenreId");

            migrationBuilder.AddColumn<int>(
                name: "SubGenreId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Artists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGenres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SubGenreId",
                table: "Events",
                column: "SubGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_GenreId",
                table: "Artists",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGenres_ExternalId",
                table: "SubGenres",
                column: "ExternalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genres_GenreId",
                table: "Artists",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_SubGenres_subGenreId",
                table: "Artists",
                column: "subGenreId",
                principalTable: "SubGenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SubGenres_SubGenreId",
                table: "Events",
                column: "SubGenreId",
                principalTable: "SubGenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genres_GenreId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_SubGenres_subGenreId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_SubGenres_SubGenreId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "SubGenres");

            migrationBuilder.DropIndex(
                name: "IX_Events_SubGenreId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Artists_GenreId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "SubGenreId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "subGenreId",
                table: "Artists",
                newName: "ArtistGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Artists_subGenreId",
                table: "Artists",
                newName: "IX_Artists_ArtistGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genres_ArtistGenreId",
                table: "Artists",
                column: "ArtistGenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
