using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class AddedContainerExecutesAlgorithms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContainerExecutesAlgorithms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConatinerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessfullyComplted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerExecutesAlgorithms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerExecutesAlgorithms_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerExecutesAlgorithms_Containers_ConatinerId",
                        column: x => x.ConatinerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerExecutesAlgorithms_AlgorithmId",
                table: "ContainerExecutesAlgorithms",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerExecutesAlgorithms_ConatinerId",
                table: "ContainerExecutesAlgorithms",
                column: "ConatinerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerExecutesAlgorithms");
        }
    }
}
