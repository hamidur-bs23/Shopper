using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoBS23.DAL.migrations.auth
{
    public partial class modify_identityAuthUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "AuthRefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 26, 16, 26, 46, 399, DateTimeKind.Utc).AddTicks(8041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 26, 13, 7, 11, 361, DateTimeKind.Utc).AddTicks(9847));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "AuthRefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 26, 13, 7, 11, 361, DateTimeKind.Utc).AddTicks(9847),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 26, 16, 26, 46, 399, DateTimeKind.Utc).AddTicks(8041));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
