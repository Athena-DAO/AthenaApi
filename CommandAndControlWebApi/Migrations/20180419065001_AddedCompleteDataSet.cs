using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class AddedCompleteDataSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompleteDataSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XComponentDataSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    YComponentDataSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteDataSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompleteDataSets_DataSets_XComponentDataSetId",
                        column: x => x.XComponentDataSetId,
                        principalTable: "DataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompleteDataSets_DataSets_YComponentDataSetId",
                        column: x => x.YComponentDataSetId,
                        principalTable: "DataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfilesCompleteDataSets",
                columns: table => new
                {
                    CompleteDataSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilesCompleteDataSets", x => new { x.CompleteDataSetId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfilesCompleteDataSets_CompleteDataSets_CompleteDataSetId",
                        column: x => x.CompleteDataSetId,
                        principalTable: "CompleteDataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilesCompleteDataSets_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompleteDataSets_XComponentDataSetId",
                table: "CompleteDataSets",
                column: "XComponentDataSetId");

            migrationBuilder.CreateIndex(
                name: "IX_CompleteDataSets_YComponentDataSetId",
                table: "CompleteDataSets",
                column: "YComponentDataSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilesCompleteDataSets_ProfileId",
                table: "ProfilesCompleteDataSets",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilesCompleteDataSets");

            migrationBuilder.DropTable(
                name: "CompleteDataSets");
        }
    }
}
