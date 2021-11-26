using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoBS23.DAL.migrations.auth
{
    public partial class modify_identityAuthUser_validateEmailAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "AuthRefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 26, 17, 29, 29, 778, DateTimeKind.Utc).AddTicks(9308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 26, 16, 26, 46, 399, DateTimeKind.Utc).AddTicks(8041));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "AuthRefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 26, 16, 26, 46, 399, DateTimeKind.Utc).AddTicks(8041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 26, 17, 29, 29, 778, DateTimeKind.Utc).AddTicks(9308));
        }
    }
}
