using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskAbout.Migrations
{
    public partial class Attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Replies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Comments");
        }
    }
}
