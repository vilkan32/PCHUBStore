using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class onlinehubusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnlineHubUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    DisconnectionDate = table.Column<DateTime>(nullable: true),
                    ActivationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineHubUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineHubUsers");
        }
    }
}
