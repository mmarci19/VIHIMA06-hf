using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaffStore.Dal.Migrations
{
    public partial class CiffAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UploadedFiles_UploadedFileId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "JsonRoute",
                table: "UploadedFiles",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "UploadedFileId",
                table: "Comment",
                newName: "CaffFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UploadedFileId",
                table: "Comment",
                newName: "IX_Comment_CaffFileId");

            migrationBuilder.CreateTable(
                name: "CiffFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaffFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiffFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CiffFile_UploadedFiles_CaffFileId",
                        column: x => x.CaffFileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    CiffFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => new { x.CiffFileId, x.Id });
                    table.ForeignKey(
                        name: "FK_Tag_CiffFile_CiffFileId",
                        column: x => x.CiffFileId,
                        principalTable: "CiffFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CiffFile_CaffFileId",
                table: "CiffFile",
                column: "CaffFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UploadedFiles_CaffFileId",
                table: "Comment",
                column: "CaffFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UploadedFiles_CaffFileId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "CiffFile");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "UploadedFiles",
                newName: "JsonRoute");

            migrationBuilder.RenameColumn(
                name: "CaffFileId",
                table: "Comment",
                newName: "UploadedFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CaffFileId",
                table: "Comment",
                newName: "IX_Comment_UploadedFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UploadedFiles_UploadedFileId",
                table: "Comment",
                column: "UploadedFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
