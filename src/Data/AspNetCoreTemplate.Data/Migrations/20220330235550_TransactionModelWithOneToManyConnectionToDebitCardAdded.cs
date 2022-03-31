using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class TransactionModelWithOneToManyConnectionToDebitCardAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionCurrency = table.Column<int>(type: "int", nullable: false),
                    Receipt = table.Column<double>(type: "float", nullable: false),
                    Payment = table.Column<double>(type: "float", nullable: false),
                    DebitCardId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_DebitCard_DebitCardId",
                        column: x => x.DebitCardId,
                        principalTable: "DebitCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebitCardId",
                table: "Transactions",
                column: "DebitCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
