using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class change_CourseIdForCourseVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseVotes_Coueses_CoruseId",
                table: "CourseVotes");

            migrationBuilder.RenameColumn(
                name: "CoruseId",
                table: "CourseVotes",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseVotes_CoruseId",
                table: "CourseVotes",
                newName: "IX_CourseVotes_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVotes_Coueses_CourseId",
                table: "CourseVotes",
                column: "CourseId",
                principalTable: "Coueses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseVotes_Coueses_CourseId",
                table: "CourseVotes");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseVotes",
                newName: "CoruseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseVotes_CourseId",
                table: "CourseVotes",
                newName: "IX_CourseVotes_CoruseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVotes_Coueses_CoruseId",
                table: "CourseVotes",
                column: "CoruseId",
                principalTable: "Coueses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
