using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todos.API.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Todos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ListId",
                table: "Todos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
