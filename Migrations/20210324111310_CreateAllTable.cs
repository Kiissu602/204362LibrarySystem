using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _204362LibrarySystem.Migrations
{
    public partial class CreateAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WriterName",
                table: "Writer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuleID",
                table: "Job",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BBR",
                columns: table => new
                {
                    BorrowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<string>(type: "char(9)", nullable: false),
                    ISBN = table.Column<string>(type: "char(13)", nullable: false),
                    BorrowDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    PhoneTemp = table.Column<string>(type: "char(10)", nullable: false),
                    ReservePlace = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BBR", x => x.BorrowID);
                    table.ForeignKey(
                        name: "FK_BBR_Book_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Book",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BBR_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckMember",
                columns: table => new
                {
                    CheckMemberID = table.Column<string>(nullable: false),
                    MemberID = table.Column<string>(type: "char(9)", nullable: true),
                    CheckStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckMember", x => x.CheckMemberID);
                    table.ForeignKey(
                        name: "FK_CheckMember_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    RuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(nullable: false),
                    LimitDayBorrow = table.Column<int>(nullable: false),
                    LimitDayBooking = table.Column<int>(nullable: false),
                    BookingTime = table.Column<TimeSpan>(nullable: false),
                    ReturnFines = table.Column<int>(nullable: false),
                    LostFines = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.RuleID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_RuleID",
                table: "Job",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_BBR_ISBN",
                table: "BBR",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_BBR_MemberID",
                table: "BBR",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckMember_MemberID",
                table: "CheckMember",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Rule_RuleID",
                table: "Job",
                column: "RuleID",
                principalTable: "Rule",
                principalColumn: "RuleID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Rule_RuleID",
                table: "Job");

            migrationBuilder.DropTable(
                name: "BBR");

            migrationBuilder.DropTable(
                name: "CheckMember");

            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Job_RuleID",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "RuleID",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "WriterName",
                table: "Writer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
