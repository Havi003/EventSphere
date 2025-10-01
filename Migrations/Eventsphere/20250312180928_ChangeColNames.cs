using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventsphere.Migrations.Eventsphere
{
    /// <inheritdoc />
    public partial class ChangeColNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EventsFormed",
                newName: "FormEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FormEventId",
                table: "EventsFormed",
                newName: "Id");
        }
    }
}
