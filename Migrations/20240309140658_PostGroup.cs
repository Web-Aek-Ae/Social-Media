using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Social_Media.Migrations
{
    /// <inheritdoc />
    public partial class PostGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostGroupPostId",
                table: "PostLikes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostGroupPostId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostGroups",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    MaxPeople = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroups", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostGroupPostId",
                table: "PostLikes",
                column: "PostGroupPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostGroupPostId",
                table: "Comments",
                column: "PostGroupPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroups_GroupId",
                table: "PostGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroups_UserId",
                table: "PostGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_PostGroups_PostGroupPostId",
                table: "Comments",
                column: "PostGroupPostId",
                principalTable: "PostGroups",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_PostGroups_PostGroupPostId",
                table: "PostLikes",
                column: "PostGroupPostId",
                principalTable: "PostGroups",
                principalColumn: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_PostGroups_PostGroupPostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_PostGroups_PostGroupPostId",
                table: "PostLikes");

            migrationBuilder.DropTable(
                name: "PostGroups");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostGroupPostId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostGroupPostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostGroupPostId",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "PostGroupPostId",
                table: "Comments");
        }
    }
}
