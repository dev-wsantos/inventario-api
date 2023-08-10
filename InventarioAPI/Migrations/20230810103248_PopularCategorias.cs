using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioAPI.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CATEGORIAS (Nome, Situacao, ImagemUrl) Values('Bebidas', 1, 'bebidas.jpg')");
            migrationBuilder.Sql("INSERT INTO CATEGORIAS (Nome, Situacao, ImagemUrl) Values('Lanches', 1, 'lanches.jpg')");
            migrationBuilder.Sql("INSERT INTO CATEGORIAS (Nome, Situacao, ImagemUrl) Values('Sobremesas', 0, 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CATEGORIAS");
        }
    }
}
