using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaffStore.Dal.Migrations
{
    public partial class CaffRouteStored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_CaffFiles_CaffFileId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CaffImage",
                table: "CaffFiles");

            migrationBuilder.DropColumn(
                name: "GifImage",
                table: "CaffFiles");

            migrationBuilder.RenameColumn(
                name: "CaffFileId",
                table: "Comment",
                newName: "UploadedFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CaffFileId",
                table: "Comment",
                newName: "IX_Comment_UploadedFileId");

            migrationBuilder.AddColumn<string>(
                name: "CaffRoute",
                table: "CaffFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GifRoute",
                table: "CaffFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_CaffFiles_UploadedFileId",
                table: "Comment",
                column: "UploadedFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_CaffFiles_UploadedFileId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CaffRoute",
                table: "CaffFiles");

            migrationBuilder.DropColumn(
                name: "GifRoute",
                table: "CaffFiles");

            migrationBuilder.RenameColumn(
                name: "UploadedFileId",
                table: "Comment",
                newName: "CaffFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UploadedFileId",
                table: "Comment",
                newName: "IX_Comment_CaffFileId");

            migrationBuilder.AddColumn<byte[]>(
                name: "CaffImage",
                table: "CaffFiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "GifImage",
                table: "CaffFiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_CaffFiles_CaffFileId",
                table: "Comment",
                column: "CaffFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
