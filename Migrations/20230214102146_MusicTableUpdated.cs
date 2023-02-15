using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dt191Gmoment3.Migrations
{
    /// <inheritdoc />
    public partial class MusicTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowedDate",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "BorrowedTo",
                table: "Music");

            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowed",
                table: "Music",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBorrowed",
                table: "Music");

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowedDate",
                table: "Music",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedTo",
                table: "Music",
                type: "TEXT",
                nullable: true);
        }
    }
}
