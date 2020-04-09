using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class activeUserOnlineHub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationDate",
                table: "OnlineHubUsers");

            migrationBuilder.DropColumn(
                name: "DisconnectionDate",
                table: "OnlineHubUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "OnlineHubUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "OnlineHubUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationDate",
                table: "OnlineHubUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DisconnectionDate",
                table: "OnlineHubUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
