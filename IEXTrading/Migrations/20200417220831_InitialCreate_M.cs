using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IEXTrading.Migrations
{
    public partial class InitialCreate_M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equities_Companies_Companyactivity",
                table: "Equities");

            migrationBuilder.DropIndex(
                name: "IX_Equities_Companyactivity",
                table: "Equities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Companyactivity",
                table: "Equities");

            migrationBuilder.AddColumn<Guid>(
                name: "RecId",
                table: "Equities",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "activity",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Companies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    activity = table.Column<string>(nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(nullable: false),
                    days_of_week = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    recreation_center = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    times = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equities_RecId",
                table: "Equities",
                column: "RecId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equities_Companies_RecId",
                table: "Equities",
                column: "RecId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equities_Companies_RecId",
                table: "Equities");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Equities_RecId",
                table: "Equities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RecId",
                table: "Equities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Companyactivity",
                table: "Equities",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "activity",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "activity");

            migrationBuilder.CreateIndex(
                name: "IX_Equities_Companyactivity",
                table: "Equities",
                column: "Companyactivity");

            migrationBuilder.AddForeignKey(
                name: "FK_Equities_Companies_Companyactivity",
                table: "Equities",
                column: "Companyactivity",
                principalTable: "Companies",
                principalColumn: "activity",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
