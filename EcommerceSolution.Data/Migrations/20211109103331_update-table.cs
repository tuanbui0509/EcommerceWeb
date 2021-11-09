using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.Data.Migrations
{
    public partial class updatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "eb2b94d5-c50f-4f2b-bb1a-94048379cf54");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ce7e4c06-84b0-4114-912f-7e27f245dc47"),
                column: "ConcurrencyStamp",
                value: "964f9f95-f47c-4a05-8f77-d017dc1e977e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "502ee21f-c5cb-4df3-acfe-c71f6f63ebed", "AQAAAAEAACcQAAAAEFuYmpfVdKbdmBvuldydzu4LOuDvMgcrNZJNtLI7D8oqJ29zo0m86PYQJKL9Zp0Fvg==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa16f18b-95b9-4e6a-837e-efb5b8e63e84"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5670ab8c-1459-4299-8608-36eb479f1b59", "AQAAAAEAACcQAAAAEM/5cFaYjmZVr/q9TaLALyruWlwxyZpet1j+R3RpwJgXdh2Kdlfv0lf2tevZdUQ+HA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AppUserId",
                table: "Carts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");
        }
    }
}
