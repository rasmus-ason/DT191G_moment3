using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dt191Gmoment3.Migrations.Borrow
{
    /// <inheritdoc />
    public partial class UpdateNameBorrowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Borrow");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowedTo",
                table: "Borrow",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedArtist",
                table: "Borrow",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BorrowedTitle",
                table: "Borrow",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowedArtist",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "BorrowedTitle",
                table: "Borrow");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowedTo",
                table: "Borrow",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Borrow",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Borrow",
                type: "TEXT",
                nullable: true);
        }
    }
}
