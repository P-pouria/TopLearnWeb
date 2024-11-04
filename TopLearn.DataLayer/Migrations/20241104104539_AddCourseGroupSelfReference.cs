using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseGroupSelfReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId");
        }
    }
}
