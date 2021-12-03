using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaffStore.Dal.Migrations
{
    public partial class RandomFileNameOnUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CaffFiles_CaffFileId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CaffFileId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "CaffFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CaffFiles_CaffFileId",
                table: "Comments",
                column: "CaffFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CaffFiles_CaffFileId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "CaffFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "CaffFileId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CaffFiles_CaffFileId",
                table: "Comments",
                column: "CaffFileId",
                principalTable: "CaffFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
