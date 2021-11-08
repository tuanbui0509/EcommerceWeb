using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.Data.Migrations
{
    public partial class updatetableproductimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "51d8a877-b983-4193-a86d-d95a133e767b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "c3a5571c-2f7c-4eef-b440-cedf5aab710a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "73aff5e9-7337-4b74-972f-c2ea231c99b9", "AQAAAAEAACcQAAAAEM+WNHLRT8cLDNrJKMKYe2YjG3gAlIMqHX0q4hNhYtt7gSkeEMy/AVl+SSVSIBRZwQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3283964-aa47-4e16-b362-25260e855eb6", "AQAAAAEAACcQAAAAEN9vg653DJ4Qu8o2al1kboACmqnB1NrDAum1rFm8j4AnsHlP2yFbbQlAJcQN6z5nyg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ProductImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4bb11c85-b992-485a-92f4-d22f938a14df");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "627062c6-6a72-46d8-9c5f-ef986cdb76a5");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f41616d5-bb07-40d5-b28e-18dbd873df6a", "AQAAAAEAACcQAAAAEJgZFXHd05T+O+j7XjMUlI7GC4/FfSklm6np3Eg370A5/ElH0dsxFVDTvgKc5DSqTQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "479041cc-2ca4-42e7-b05b-cd88a585a91a", "AQAAAAEAACcQAAAAEMJwKotLhg958Lxve4WqeXCh2tX7QyZm+cZ0guTzr8zk9T+bng6rHW4jb09tZdhiYA==" });
        }
    }
}
