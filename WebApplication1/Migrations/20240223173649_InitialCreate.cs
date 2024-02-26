using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "filmListDB",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    setAge = table.Column<int>(type: "int", nullable: false),
                    imbd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmListDB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sessionsDB",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessionsDB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PackageOfCinemaDB",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFilms = table.Column<int>(type: "int", nullable: false),
                    idSessions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sessionsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageOfCinemaDB", x => x.id);
                    table.ForeignKey(
                        name: "FK_PackageOfCinemaDB_filmListDB_idFilms",
                        column: x => x.idFilms,
                        principalTable: "filmListDB",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageOfCinemaDB_sessionsDB_Sessionsid",
                        column: x => x.Sessionsid,
                        principalTable: "sessionsDB",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageOfCinemaDB_idFilms",
                table: "PackageOfCinemaDB",
                column: "idFilms");

            migrationBuilder.CreateIndex(
                name: "IX_PackageOfCinemaDB_Sessionsid",
                table: "PackageOfCinemaDB",
                column: "Sessionsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageOfCinemaDB");

            migrationBuilder.DropTable(
                name: "filmListDB");

            migrationBuilder.DropTable(
                name: "sessionsDB");
        }
    }
}
