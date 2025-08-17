using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    /// <inheritdoc />
    public partial class LikeRemoveIssue1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLike_BlogPosts_BlogPostId",
                table: "BlogPostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostLike",
                table: "BlogPostLike");

            //migrationBuilder.RenameTable(
            //    name: "BlogPostLike",
            //    newName: "BlogPostLikes");

            //migrationBuilder.RenameIndex(
            //    name: "IX_BlogPostLike_BlogPostId",
            //    table: "BlogPostLikes",
            //    newName: "IX_BlogPostLikes_BlogPostId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_BlogPostLikes",
            //    table: "BlogPostLikes",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
            //    table: "BlogPostLikes",
            //    column: "BlogPostId",
            //    principalTable: "BlogPosts",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostLikes",
                table: "BlogPostLikes");

            migrationBuilder.RenameTable(
                name: "BlogPostLikes",
                newName: "BlogPostLike");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "BlogPostLike",
                newName: "IX_BlogPostLike_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostLike",
                table: "BlogPostLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLike_BlogPosts_BlogPostId",
                table: "BlogPostLike",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
