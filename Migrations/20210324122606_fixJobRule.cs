﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace _204362LibrarySystem.Migrations
{
    public partial class fixJobRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobID",
                table: "Rule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_JobID",
                table: "Rule",
                column: "JobID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Job_JobID",
                table: "Rule",
                column: "JobID",
                principalTable: "Job",
                principalColumn: "JobID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Job_JobID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_JobID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "JobID",
                table: "Rule");
        }
    }
}
