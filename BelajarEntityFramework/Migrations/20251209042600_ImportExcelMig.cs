using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelajarEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ImportExcelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "ProductsExcel",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsExcel", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductsExcel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsExcel_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic gadgets and devices", "Electronics" },
                    { 2, "Clothing and fashion accessories", "Apparel" },
                    { 3, "Furniture and home decor", "Home Goods" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "SupplierId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "SupplierId",
                value: null);

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "ContactEmail", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "contact@techsource.com", "TechSource", "555-1234" },
                    { 2, "sales@fashionhub.com", "FashionHub", "555-5678" }
                });

            migrationBuilder.InsertData(
                table: "ProductsExcel",
                columns: new[] { "ProductId", "Brand", "CategoryId", "Description", "DiscountPercentage", "IsActive", "Name", "Price", "Quantity", "SKU", "SupplierId" },
                values: new object[,]
                {
                    { 1, "SoundMax", 1, "Noise-cancelling over-ear headphones", 10m, true, "Wireless Headphones", 99.99m, 50, "ELE-SOU-WIR-TEC-2025", 1 },
                    { 2, "FashionCo", 2, "100% cotton, trendy design", 5m, true, "Designer T-Shirt", 29.99m, 150, "APP-FAS-DES-FAS-2025", 2 },
                    { 3, "VisionTech", 1, "50-inch 4K Ultra HD Smart LED TV", 15m, true, "Smart LED TV", 499.99m, 30, "ELE-VIS-SMA-TEC-2025", 1 },
                    { 4, "HomeComfort", 3, "Comfortable 3-seater sofa", 8m, true, "Modern Sofa", 299.99m, 20, "HOM-HOM-MOD-FAS-2025", 2 },
                    { 5, "SoundMax", 1, "Portable wireless speaker with deep bass", 12m, true, "Bluetooth Speaker", 49.99m, 80, "ELE-SOU-BLU-TEC-2025", 1 },
                    { 6, "", 2, "Comfortable and stylish sneakers", 0m, true, "Casual Sneakers", 59.99m, 100, "APP-BRD-CAS-FAS-2025", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsExcel_CategoryId",
                table: "ProductsExcel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsExcel_SupplierId",
                table: "ProductsExcel",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductsExcel");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Products");
        }
    }
}
