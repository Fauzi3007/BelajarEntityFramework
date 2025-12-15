using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelajarEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ChartMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersChart",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersChart", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ProductsChart",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsChart", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "OrdersChart",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersChart", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrdersChart_CustomersChart_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomersChart",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemsChart",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemsChart", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItemsChart_OrdersChart_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersChart",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemsChart_ProductsChart_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsChart",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 13,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 14,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 15,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 16,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 17,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 18,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 19,
                column: "Date",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 20,
                column: "Date",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "CustomersChart",
                columns: new[] { "CustomerId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "Alice" },
                    { 2, "bob@example.com", "Bob" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "ProductsChart",
                columns: new[] { "ProductId", "Category", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Electronics", "Laptop", 1200m, 10 },
                    { 2, "Electronics", "Phone", 800m, 15 },
                    { 3, "Accessories", "Headphones", 100m, 30 }
                });

            migrationBuilder.InsertData(
                table: "OrdersChart",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "OrderNumber", "OrderStatus", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0001", 2, 1300m },
                    { 2, 2, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0002", 1, 2200m },
                    { 3, 1, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0003", 3, 500m },
                    { 4, 2, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0004", 2, 1600m },
                    { 5, 1, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0005", 1, 900m },
                    { 6, 2, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0006", 2, 1800m },
                    { 7, 1, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0007", 2, 1100m },
                    { 8, 2, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0008", 3, 2500m },
                    { 9, 1, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0009", 1, 1700m },
                    { 10, 2, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0010", 2, 1400m },
                    { 11, 1, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0011", 2, 2000m },
                    { 12, 2, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0012", 1, 2100m },
                    { 13, 1, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0013", 2, 1300m },
                    { 14, 2, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0014", 3, 1900m },
                    { 15, 1, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0015", 2, 1500m },
                    { 16, 1, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0016", 1, 300m },
                    { 17, 2, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0017", 2, 800m },
                    { 18, 1, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "ORD-0018", 3, 150m }
                });

            migrationBuilder.InsertData(
                table: "OrderItemsChart",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1200m },
                    { 2, 1, 3, 1, 100m },
                    { 3, 2, 1, 1, 1200m },
                    { 4, 2, 2, 1, 1000m },
                    { 5, 3, 3, 5, 100m },
                    { 6, 4, 2, 2, 800m },
                    { 7, 5, 3, 3, 300m },
                    { 8, 6, 1, 1, 1200m },
                    { 9, 6, 3, 1, 600m },
                    { 10, 7, 2, 1, 1100m },
                    { 11, 8, 1, 1, 2500m },
                    { 12, 9, 3, 2, 850m },
                    { 13, 10, 2, 1, 1400m },
                    { 14, 11, 1, 1, 2000m },
                    { 15, 12, 2, 1, 2100m },
                    { 16, 13, 3, 1, 1300m },
                    { 17, 14, 1, 1, 1900m },
                    { 18, 15, 2, 1, 1500m },
                    { 19, 16, 3, 3, 100m },
                    { 20, 17, 2, 1, 800m },
                    { 21, 18, 3, 1, 150m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsChart_OrderId",
                table: "OrderItemsChart",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsChart_ProductId",
                table: "OrderItemsChart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersChart_CustomerId",
                table: "OrdersChart",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemsChart");

            migrationBuilder.DropTable(
                name: "OrdersChart");

            migrationBuilder.DropTable(
                name: "ProductsChart");

            migrationBuilder.DropTable(
                name: "CustomersChart");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 13,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 14,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 15,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 16,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 17,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 18,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 19,
                column: "Date",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 20,
                column: "Date",
                value: new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
