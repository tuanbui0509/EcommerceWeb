using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.Data.Migrations
{
    public partial class updatedatabasetableorderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b5e953c9-479b-4061-ba1d-738eb503e07f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "11faf0a1-5c8a-4174-a849-a400dc630698");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "abb9dd3b-076c-4d6f-a381-66fdc81a7a59", "AQAAAAEAACcQAAAAEELnVfqQVptWac/Q9MmItuvbSNIiAqrBKxuB1NALvLs8VVHPkC1I4ycGcgpJGODtxA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc8313dd-ce56-4924-ab8c-bc51667d9248", "AQAAAAEAACcQAAAAEMKPnokzUR0heQ7SHYkgn/dRJOGdY9LWwhbaNpXoDlDQm5EKadYnTtmqe+W1zIkxgg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
