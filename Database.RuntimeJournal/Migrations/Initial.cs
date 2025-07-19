using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.RuntimeJournal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "journal");

            migrationBuilder.CreateTable(
                name: "journals_n4",
                schema: "journal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JournalParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    JournalHeader = table.Column<string>(type: "text", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journals_n4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_journals_n4_journals_n4_JournalParentId",
                        column: x => x.JournalParentId,
                        principalSchema: "journal",
                        principalTable: "journals_n4",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_journals_n4_Id",
                schema: "journal",
                table: "journals_n4",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_journals_n4_JournalHeader_JournalParentId",
                schema: "journal",
                table: "journals_n4",
                columns: new[] { "JournalHeader", "JournalParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_journals_n4_JournalParentId",
                schema: "journal",
                table: "journals_n4",
                column: "JournalParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "journals_n4",
                schema: "journal");
        }
    }
}
