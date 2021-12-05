using Microsoft.EntityFrameworkCore.Migrations;

namespace CaffStore.Dal.Migrations
{
    public partial class StoreCiffMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CiffFile_UploadedFiles_CaffFileId",
                table: "CiffFile");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UploadedFiles_CaffFileId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "Capture",
                table: "CiffFile");

            migrationBuilder.RenameTable(
                name: "UploadedFiles",
                newName: "CaffFiles");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "CiffFile",
                newName: "Caption");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "CaffFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaffFiles",
                table: "CaffFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CiffFile_CaffFiles_CaffFileId",
                table: "CiffFile",
                column: "CaffFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_CaffFiles_CaffFileId",
                table: "Comment",
                column: "CaffFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CiffFile_CaffFiles_CaffFileId",
                table: "CiffFile");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_CaffFiles_CaffFileId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaffFiles",
                table: "CaffFiles");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "CaffFiles");

            migrationBuilder.RenameTable(
                name: "CaffFiles",
                newName: "UploadedFiles");

            migrationBuilder.RenameColumn(
                name: "Caption",
                table: "CiffFile",
                newName: "Creator");

            migrationBuilder.AddColumn<string>(
                name: "Capture",
                table: "CiffFile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CiffFile_UploadedFiles_CaffFileId",
                table: "CiffFile",
                column: "CaffFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UploadedFiles_CaffFileId",
                table: "Comment",
                column: "CaffFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
