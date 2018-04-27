using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class AddedMissingDataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GetCompleteDataSetId",
                table: "Pipelines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_GetCompleteDataSetId",
                table: "Pipelines",
                column: "GetCompleteDataSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pipelines_CompleteDataSets_GetCompleteDataSetId",
                table: "Pipelines",
                column: "GetCompleteDataSetId",
                principalTable: "CompleteDataSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pipelines_CompleteDataSets_GetCompleteDataSetId",
                table: "Pipelines");

            migrationBuilder.DropIndex(
                name: "IX_Pipelines_GetCompleteDataSetId",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "GetCompleteDataSetId",
                table: "Pipelines");
        }
    }
}
