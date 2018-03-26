using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class NukeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerExecutesAlgorithms");

            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Outputs");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Algorithms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MasterExecutableFileLocation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SlaveExecutableFileLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    PortNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlgorithmId = table.Column<Guid>(nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inputs_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Outputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlgorithmId = table.Column<Guid>(nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outputs_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContainerExecutesAlgorithms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlgorithmId = table.Column<Guid>(nullable: false),
                    ConatinerId = table.Column<Guid>(nullable: true),
                    ContainerId = table.Column<Guid>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    Output = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    SuccessfullyComplted = table.Column<bool>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_AlgorithmId",
                table: "Inputs",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_AlgorithmId",
                table: "Outputs",
                column: "AlgorithmId");
        }
    }
}
