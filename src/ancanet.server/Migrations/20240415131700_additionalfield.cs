using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ancanet.server.Migrations
{
    /// <inheritdoc />
    public partial class additionalfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfileConfigured",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileConfigured",
                table: "AspNetUsers");
        }
    }
}
