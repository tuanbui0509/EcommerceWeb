using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.Data.Migrations
{
    public partial class updaterowversion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desciption",
                table: "Categories",
                newName: "Description");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "Desciption");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "35dad465-e72a-439d-b0d0-4a9a0882462c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "701783f5-fe9d-4e1d-ae56-335ab59f496e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af201dea-0005-4dda-8ef6-c5ed877eeb87", "AQAAAAEAACcQAAAAEDjpFfHp4nEqlrMLg8G5PZl6WfHRVZQuhNXgn4ttq8ONrAk7+m/Uw/PYq2A4FBZI3g==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4b5ca845-75da-4811-80dd-4de0ca9f0370", "AQAAAAEAACcQAAAAEF1zPnGmx7NfsporLcX13jvT0oEP7WUf/ASU4pJLUVuD395MK1FC8s21AAdlOtxbeQ==" });
        }
    }
}
