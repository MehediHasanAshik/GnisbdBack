using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gnisbdback.Migrations
{
    /// <inheritdoc />
    public partial class meetingtablefixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Meeting_Minutes_Master_Tbl",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Meeting_Minutes_Master_Tbl",
                newName: "DateandTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateandTime",
                table: "Meeting_Minutes_Master_Tbl",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Meeting_Minutes_Master_Tbl",
                newName: "Name");
        }
    }
}
