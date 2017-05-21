using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskAbout.Migrations
{
    public partial class Questions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_QuestionId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RepliesCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "RepliesCount",
                table: "Questions",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_QuestionId",
                table: "Comments",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
