using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CommandAndControlWebApi.Migrations
{
    public partial class UpdatedPipelineAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_PipelineParameters", "PipelineParameters");
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PipelineParameters",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));
            migrationBuilder.AddPrimaryKey("PK_PipelineParameters", "PipelineParameters", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PipelineParameters",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
