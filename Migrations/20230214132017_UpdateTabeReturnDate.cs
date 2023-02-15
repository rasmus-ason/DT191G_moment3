using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dt191Gmoment3.Migrations.Borrow
{
    /// <inheritdoc />
    public partial class UpdateTabeReturnDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Borrow",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Borrow");
        }
    }
}
