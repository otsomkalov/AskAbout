using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ask.about.Migrations
{
    public partial class topics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TopicId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicTitle",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicTitle",
                table: "Questions",
                column: "TopicTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicTitle",
                table: "Questions",
                column: "TopicTitle",
                principalTable: "Topics",
                principalColumn: "Title",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicTitle",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TopicTitle",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TopicTitle",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Topics",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicId",
                table: "Questions",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
