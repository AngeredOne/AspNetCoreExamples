using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExceptionJournalApiExample.Migrations
{
    /// <inheritdoc />
    public partial class AddNodePayloadField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payload",
                table: "Nodes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payload",
                table: "Nodes");
        }
    }
}
