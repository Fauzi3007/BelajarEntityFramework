using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelajarEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProofTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProofTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureFileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureFileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityProofs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProofTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    VerificationStatusId = table.Column<int>(type: "int", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityProofs_ProofTypes_ProofTypeId",
                        column: x => x.ProofTypeId,
                        principalTable: "ProofTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityProofs_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityProofs_VerificationStatuses_VerificationStatusId",
                        column: x => x.VerificationStatusId,
                        principalTable: "VerificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProofTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Passport" },
                    { 2, "Driver License" },
                    { 3, "National ID" },
                    { 4, "Voter ID" },
                    { 5, "Aadhar" },
                    { 6, "Pan" },
                    { 7, "Other" }
                });

            migrationBuilder.InsertData(
                table: "VerificationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Verified" },
                    { 3, "Rejected" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProofs_ProofTypeId",
                table: "IdentityProofs",
                column: "ProofTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProofs_UserProfileId",
                table: "IdentityProofs",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProofs_VerificationStatusId",
                table: "IdentityProofs",
                column: "VerificationStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityProofs");

            migrationBuilder.DropTable(
                name: "ProofTypes");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "VerificationStatuses");
        }
    }
}
