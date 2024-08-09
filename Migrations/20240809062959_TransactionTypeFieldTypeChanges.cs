using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financial_manager.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTypeFieldTypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transaction_type",
                table: "transactions",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_type",
                table: "transactions");
        }
    }
}
