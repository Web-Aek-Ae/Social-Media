using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostStatus",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostStatus",
                table: "Posts");
        }
    }
}
