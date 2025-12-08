using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mamma_shopping_helper.Migrations
{
    /// <inheritdoc />
    public partial class AddDataUltimaModificaAndCreataDa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreataDa",
                table: "ListeDellaSpesa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaModifica",
                table: "ListeDellaSpesa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreataDa",
                table: "ListeDellaSpesa");

            migrationBuilder.DropColumn(
                name: "DataUltimaModifica",
                table: "ListeDellaSpesa");
        }
    }
}
