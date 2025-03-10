using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventsphere.Migrations.Eventsphere
{
    /// <inheritdoc />
    public partial class FixTicketDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetails_EventsFormed_Id",
                table: "TicketDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TicketDetails",
                newName: "FormEventId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetails_Id",
                table: "TicketDetails",
                newName: "IX_TicketDetails_FormEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetails_EventsFormed_FormEventId",
                table: "TicketDetails",
                column: "FormEventId",
                principalTable: "EventsFormed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetails_EventsFormed_FormEventId",
                table: "TicketDetails");

            migrationBuilder.RenameColumn(
                name: "FormEventId",
                table: "TicketDetails",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetails_FormEventId",
                table: "TicketDetails",
                newName: "IX_TicketDetails_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetails_EventsFormed_Id",
                table: "TicketDetails",
                column: "Id",
                principalTable: "EventsFormed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
