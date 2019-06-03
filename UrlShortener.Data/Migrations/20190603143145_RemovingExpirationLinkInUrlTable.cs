using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Data.Migrations
{
    public partial class RemovingExpirationLinkInUrlTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireLinkUrl",
                table: "Urls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpireLinkUrl",
                table: "Urls",
                nullable: true);
        }
    }
}
