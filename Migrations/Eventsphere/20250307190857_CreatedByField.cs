using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventsphere.Migrations.Eventsphere
{
    /// <inheritdoc />
    public partial class CreatedByField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EventsFormed",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EventsFormed_CreatedBy",
                table: "EventsFormed",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsFormed_AspNetUsers_CreatedBy",
                table: "EventsFormed",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsFormed_AspNetUsers_CreatedBy",
                table: "EventsFormed");

            migrationBuilder.DropIndex(
                name: "IX_EventsFormed_CreatedBy",
                table: "EventsFormed");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EventsFormed");
        }
    }
}
