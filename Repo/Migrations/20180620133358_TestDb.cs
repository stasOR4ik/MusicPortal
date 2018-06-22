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

            migrationBuilder.CreateTable(
                name: "SimilarArtists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArtistId = table.Column<int>(nullable: true),
                    SimilarArtistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimilarArtists_Artists_SimilarArtistId",
                        column: x => x.SimilarArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimilarArtists_ArtistId",
                table: "SimilarArtists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarArtists_SimilarArtistId",
                table: "SimilarArtists",
                column: "SimilarArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimilarArtists");

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
