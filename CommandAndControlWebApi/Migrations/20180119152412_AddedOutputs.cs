using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class AddedOutputs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlgorithmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_AlgorithmId",
                table: "Outputs",
                column: "AlgorithmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outputs");
        }
    }
}
