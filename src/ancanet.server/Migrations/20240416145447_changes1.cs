using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ancanet.server.Migrations
{
    /// <inheritdoc />
    public partial class changes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsProfileConfigured",
                table: "AspNetUsers",
                newName: "IsProfileSetup");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsProfileSetup",
                table: "AspNetUsers",
                newName: "IsProfileConfigured");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
