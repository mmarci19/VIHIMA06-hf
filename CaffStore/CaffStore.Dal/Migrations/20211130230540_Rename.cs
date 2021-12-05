using Microsoft.EntityFrameworkCore.Migrations;

namespace CaffStore.Dal.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_CaffFiles_UploadedFileId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaffFiles",
                table: "CaffFiles");

            migrationBuilder.RenameTable(
                name: "CaffFiles",
                newName: "UploadedFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UploadedFiles_UploadedFileId",
                table: "Comment",
                column: "UploadedFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UploadedFiles_UploadedFileId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles");

            migrationBuilder.RenameTable(
                name: "UploadedFiles",
                newName: "CaffFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaffFiles",
                table: "CaffFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_CaffFiles_UploadedFileId",
                table: "Comment",
                column: "UploadedFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
