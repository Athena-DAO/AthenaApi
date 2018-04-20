using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class AddedPipeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pipelines_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PipelineParameters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlgorithmParameterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineParameters_AlgorithmParameters_AlgorithmParameterId",
                        column: x => x.AlgorithmParameterId,
                        principalTable: "AlgorithmParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PipelineParameters_Pipelines_PipelineId",
                        column: x => x.PipelineId,
                        principalTable: "Pipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PipelineParameters_AlgorithmParameterId",
                table: "PipelineParameters",
                column: "AlgorithmParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineParameters_PipelineId",
                table: "PipelineParameters",
                column: "PipelineId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_AlgorithmId",
                table: "Pipelines",
                column: "AlgorithmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PipelineParameters");

            migrationBuilder.DropTable(
                name: "Pipelines");
        }
    }
}
