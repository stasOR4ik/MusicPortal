using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repo.Migrations
{
    public partial class TestDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_ArtistSimilarArtists_Id",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "ArtistSimilarArtists");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Artists",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Artists",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "ArtistSimilarArtists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SimilarArtistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSimilarArtists", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_ArtistSimilarArtists_Id",
                table: "Artists",
                column: "Id",
                principalTable: "ArtistSimilarArtists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
