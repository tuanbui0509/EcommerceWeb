using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.Data.Migrations
{
    public partial class updateuseraccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "8d9934c0-a89a-4270-bddd-cb3964eeda30", "Admin" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "c6f875bd-1d0b-4d34-a327-c8766d6231e7");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f63c532c-2c9e-49fa-b5ec-b248aa5c461e", "AQAAAAEAACcQAAAAEMobGV0sGbS6yRK+u9oG7bPN4SYy49aAR+eABGoY9mD4mNjqYAQX9lWXlgLK+ycoaw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4809eefa-28d6-477a-b5b9-a126acf7facf", "AQAAAAEAACcQAAAAEENieA8uu1ij2+B6R40a8EjJVJMh7+NuIt6/Ggh3wHf1fi7MGxwCeH53FyraLbUKYQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "f3d8b30f-5a6b-490d-8b0f-4f89eac28af6", "admin" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "45c32f5d-6d22-45f0-a6a2-2180d96ac7f5");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1708eb7b-270c-490b-8f42-7c1631b474b7", "AQAAAAEAACcQAAAAEDOcL3lyeEvg/4Pwrh/Sc1uuk+hn6NwZIOAvYewUMWG5LPrF5S+JXXAV+1ISut5fBQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "495b2670-dfd1-4635-a9ab-21c15b3c5ee6", "AQAAAAEAACcQAAAAEBsYo4puoMOFoI9Uqf//BMLV9gb8thq12DvHMx+pe6Hf/EH0p2T0piwXn/RrangMbA==" });
        }
    }
}
