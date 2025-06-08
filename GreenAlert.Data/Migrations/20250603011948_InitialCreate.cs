using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenAlert.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTACAO",
                columns: table => new
                {
                    EstacaoId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTACAO", x => x.EstacaoId);
                });

            migrationBuilder.CreateTable(
                name: "SENSOR",
                columns: table => new
                {
                    SensorId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Unidade = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    EstacaoId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSOR", x => x.SensorId);
                    table.ForeignKey(
                        name: "FK_SENSOR_ESTACAO_EstacaoId",
                        column: x => x.EstacaoId,
                        principalTable: "ESTACAO",
                        principalColumn: "EstacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ALERTA",
                columns: table => new
                {
                    AlertaId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    SensorId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERTA", x => x.AlertaId);
                    table.ForeignKey(
                        name: "FK_ALERTA_SENSOR_SensorId",
                        column: x => x.SensorId,
                        principalTable: "SENSOR",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_SensorId",
                table: "ALERTA",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SENSOR_EstacaoId",
                table: "SENSOR",
                column: "EstacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALERTA");

            migrationBuilder.DropTable(
                name: "SENSOR");

            migrationBuilder.DropTable(
                name: "ESTACAO");
        }
    }
}
