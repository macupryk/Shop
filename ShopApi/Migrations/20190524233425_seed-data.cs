using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserName = table.Column<string>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "CreatedDate", "CreatedUserName", "Description", "ModifiedDate", "ModifiedUserName", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1L, new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item1 Description", new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item1", 100, 10 },
                    { 2L, new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item2 Description", new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item2", 200, 5 },
                    { 3L, new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item3 Description", new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item3", 300, 50 },
                    { 4L, new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item4 Description", new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item4", 250, 15 },
                    { 5L, new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item5 Description", new DateTime(2019, 5, 24, 23, 34, 24, 543, DateTimeKind.Utc).AddTicks(3852), null, "Item5", 400, 105 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
