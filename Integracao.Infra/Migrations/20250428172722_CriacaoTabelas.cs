using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integracao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TDE_PARA_STATUS",
                columns: table => new
                {
                    ACOD_STATUS = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACOD_STATUS_Glpi = table.Column<int>(type: "int", nullable: false),
                    ADES_STATUS_GLPI = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ACOD_STATUS_DEVOPS = table.Column<int>(type: "int", nullable: false),
                    ADES_STATUS_DEVOPS = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDE_PARA_STATUS", x => x.ACOD_STATUS);
                });

            migrationBuilder.CreateTable(
                name: "TTICKETS",
                columns: table => new
                {
                    ACOD_TICKET = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACOD_TICKET_GLPI = table.Column<long>(type: "bigint", nullable: false),
                    ACOD_TICKET_DEVOPS = table.Column<long>(type: "bigint", nullable: false),
                    ACOD_STATUS = table.Column<long>(type: "bigint", nullable: false),
                    ADAT_ALTERACAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    ACOD_USUARIO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTICKETS", x => x.ACOD_TICKET);
                });

            migrationBuilder.CreateTable(
                name: "TUSUARIOS",
                columns: table => new
                {
                    ACOD_USUARIO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADES_EMAIL = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ADES_USUARIO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUSUARIOS", x => x.ACOD_USUARIO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TDE_PARA_STATUS");

            migrationBuilder.DropTable(
                name: "TTICKETS");

            migrationBuilder.DropTable(
                name: "TUSUARIOS");
        }
    }
}
