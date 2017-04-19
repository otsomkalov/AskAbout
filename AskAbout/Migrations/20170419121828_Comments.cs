using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskAbout.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Questions_QuestionId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Replies_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Comment_DateId_UserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comments",
                newName: "IX_Comments_ReplyUserId_ReplyQuestionId_ReplyDate");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId1",
                table: "Comments",
                newName: "IX_Comments_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_QuestionId",
                table: "Comments",
                newName: "IX_Comments_QuestionId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Comments_DateId_UserId",
                table: "Comments",
                columns: new[] { "DateId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                columns: new[] { "UserId", "DateId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Replies_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comments",
                columns: new[] { "ReplyUserId", "ReplyQuestionId", "ReplyDate" },
                principalTable: "Replies",
                principalColumns: new[] { "UserId", "QuestionId", "Date" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Replies_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Comments_DateId_UserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comment",
                newName: "IX_Comment_ReplyUserId_ReplyQuestionId_ReplyDate");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId1",
                table: "Comment",
                newName: "IX_Comment_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_QuestionId",
                table: "Comment",
                newName: "IX_Comment_QuestionId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Comment_DateId_UserId",
                table: "Comment",
                columns: new[] { "DateId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                columns: new[] { "UserId", "DateId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Questions_QuestionId",
                table: "Comment",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId1",
                table: "Comment",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Replies_ReplyUserId_ReplyQuestionId_ReplyDate",
                table: "Comment",
                columns: new[] { "ReplyUserId", "ReplyQuestionId", "ReplyDate" },
                principalTable: "Replies",
                principalColumns: new[] { "UserId", "QuestionId", "Date" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
