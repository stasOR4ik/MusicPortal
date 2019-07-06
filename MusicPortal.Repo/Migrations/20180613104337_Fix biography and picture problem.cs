using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MusicPortal.Repo.Migrations
{
    public partial class Fixbiographyandpictureproblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Tracks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "Tracks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortBiography",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "Albums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortBiography",
                table: "Albums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "ShortBiography",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ShortBiography",
                table: "Albums");
        }
    }
}
