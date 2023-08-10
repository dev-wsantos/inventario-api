using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioAPI.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder builder)
        {

            builder.Sql("INSERT INTO Categorias(Nome, Situacao, ImageUrl) Values('Informática', 1, 'informatica.jpg')");
            builder.Sql("Insert INTO Categorias(Nome, Situacao, ImageUrl) Values('Eletrodomésticos', 0,'eletrodomesticos.jpg')");
            builder.Sql("INSERT INTO Categorias(Nome, Situacao, ImageUrl) Values('Livros', 1, 'livros.jpg')");
        }

        protected override void Down(MigrationBuilder builder)
        {
            builder.Sql("DELETE FROM Categorias");
        }
    }
}
