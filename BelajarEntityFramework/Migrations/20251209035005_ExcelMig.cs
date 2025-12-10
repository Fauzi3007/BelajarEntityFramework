using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelajarEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ExcelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentsExcel",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsExcel", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.EmployeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesExcel",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesExcel", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeesExcel_DepartmentsExcel_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentsExcel",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesExcel_EmployeeTypes_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeTypes",
                        principalColumn: "EmployeeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_EmployeesExcel_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeesExcel",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DepartmentsExcel",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, "HR" },
                    { 2, "Finance" },
                    { 3, "IT" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "EmployeeTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Full Time" },
                    { 2, "Part Time" },
                    { 3, "Contractor" }
                });

            migrationBuilder.InsertData(
                table: "EmployeesExcel",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "EmployeeTypeId", "FirstName", "IsActive", "JoinDate", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, 1, "john.doe@example.com", 1, "John", true, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", 60000m },
                    { 2, 2, "jane.smith@example.com", 2, "Jane", true, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", 40000m },
                    { 3, 3, "bob.johnson@example.com", 3, "Bob", false, new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Johnson", 50000m },
                    { 4, 1, "alice.williams@example.com", 1, "Alice", true, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Williams", 65000m },
                    { 5, 2, "michael.brown@example.com", 2, "Michael", true, new DateTime(2018, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brown", 55000m },
                    { 6, 3, "linda.davis@example.com", 3, "Linda", false, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davis", 48000m },
                    { 7, 1, "david.miller@example.com", 1, "David", true, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miller", 62000m },
                    { 8, 2, "susan.wilson@example.com", 2, "Susan", true, new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wilson", 43000m },
                    { 9, 3, "robert.moore@example.com", 3, "Robert", false, new DateTime(2022, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moore", 51000m },
                    { 10, 1, "karen.taylor@example.com", 1, "Karen", true, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taylor", 70000m }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "Date", "EmployeeId", "IsPresent" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, true },
                    { 2, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, true },
                    { 3, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, true },
                    { 4, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 2, false },
                    { 5, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 3, false },
                    { 6, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 3, true },
                    { 7, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 4, true },
                    { 8, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 4, true },
                    { 9, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 5, true },
                    { 10, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 5, false },
                    { 11, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 6, false },
                    { 12, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 6, true },
                    { 13, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 7, true },
                    { 14, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 7, true },
                    { 15, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 8, true },
                    { 16, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 8, false },
                    { 17, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 9, false },
                    { 18, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 9, true },
                    { 19, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 10, true },
                    { 20, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Local), 10, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesExcel_DepartmentId",
                table: "EmployeesExcel",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesExcel_EmployeeTypeId",
                table: "EmployeesExcel",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "EmployeesExcel");

            migrationBuilder.DropTable(
                name: "DepartmentsExcel");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "Employees");
        }
    }
}
