using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevCars.API.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true, defaultValue: "Padrão"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataProducao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Registrado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCar = table.Column<int>(type: "int", nullable: false),
                    idCustomer = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Carros_IdCar",
                        column: x => x.IdCar,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pedidoId = table.Column<int>(type: "int", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Pedido_pedidoId",
                        column: x => x.pedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedidosExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidosExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedidosExtras_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_pedidoId",
                table: "Clientes",
                column: "pedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidosExtras_IdPedido",
                table: "ItensPedidosExtras",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdCar",
                table: "Pedido",
                column: "IdCar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_idCustomer",
                table: "Pedido",
                column: "idCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Clientes_idCustomer",
                table: "Pedido",
                column: "idCustomer",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Pedido_pedidoId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "ItensPedidosExtras");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
