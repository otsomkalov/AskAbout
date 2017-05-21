using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskAbout.Migrations
{
    public partial class Replies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentariesCount",
                table: "Replies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentariesCount",
                table: "Replies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
