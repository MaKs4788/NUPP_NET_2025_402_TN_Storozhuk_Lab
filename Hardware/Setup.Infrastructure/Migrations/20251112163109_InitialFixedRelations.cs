using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Setup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialFixedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RAM = table.Column<int>(type: "INTEGER", nullable: false),
                    Storage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardware", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peripheries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ConnectionType = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ComputerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripheries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peripheries_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OS = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OSVersion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Antivirus = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ComputerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Softwares_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cores = table.Column<int>(type: "INTEGER", nullable: false),
                    Threads = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequency = table.Column<double>(type: "REAL", nullable: false),
                    ComputerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUs_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUs_Hardware_Id",
                        column: x => x.Id,
                        principalTable: "Hardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VRAM = table.Column<int>(type: "INTEGER", nullable: false),
                    MemoryType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CoreClock = table.Column<double>(type: "REAL", nullable: false),
                    ComputerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUs_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GPUs_Hardware_Id",
                        column: x => x.Id,
                        principalTable: "Hardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_ComputerId",
                table: "CPUs",
                column: "ComputerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_ComputerId",
                table: "GPUs",
                column: "ComputerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peripheries_ComputerId",
                table: "Peripheries",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_ComputerId",
                table: "Softwares",
                column: "ComputerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.DropTable(
                name: "Peripheries");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Hardware");

            migrationBuilder.DropTable(
                name: "Computers");
        }
    }
}
