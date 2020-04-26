using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IEXTrading.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    activity = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    age_requirements = table.Column<string>(nullable: true),
                    days_of_week = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    recreation_center = table.Column<string>(nullable: true),
                    times = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.activity);
                });

            migrationBuilder.CreateTable(
                name: "Equities",
                columns: table => new
                {
                    EquityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Companyactivity = table.Column<string>(nullable: true),
                    change = table.Column<float>(nullable: false),
                    changeOverTime = table.Column<float>(nullable: false),
                    changePercent = table.Column<float>(nullable: false),
                    close = table.Column<float>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    high = table.Column<float>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    low = table.Column<float>(nullable: false),
                    open = table.Column<float>(nullable: false),
                    symbol = table.Column<string>(nullable: true),
                    unadjustedVolume = table.Column<int>(nullable: false),
                    volume = table.Column<int>(nullable: false),
                    vwap = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equities", x => x.EquityId);
                    table.ForeignKey(
                        name: "FK_Equities_Companies_Companyactivity",
                        column: x => x.Companyactivity,
                        principalTable: "Companies",
                        principalColumn: "activity",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equities_Companyactivity",
                table: "Equities",
                column: "Companyactivity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equities");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
