using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dt191Gmoment3.Migrations
{
    /// <inheritdoc />
    public partial class MigrationWithDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowedDate",
                table: "Music",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowedDate",
                table: "Music");
        }
    }
}
