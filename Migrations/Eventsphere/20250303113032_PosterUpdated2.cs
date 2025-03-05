using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventsphere.Migrations.Eventsphere
{
    /// <inheritdoc />
    public partial class PosterUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<byte[]>(
                name: "PosterTemp",
                table: "EventsFormed",
                type: "varbinary(max)",
                nullable: true); 

            migrationBuilder.Sql("UPDATE EventsFormed SET PosterTemp = CONVERT(VARBINARY(MAX), Poster) WHERE Poster IS NOT NULL");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "EventsFormed");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PosterTemp",
                table: "EventsFormed",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "PosterTemp",
                table: "EventsFormed",
                newName: "Poster");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Poster",
                table: "EventsFormed",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
