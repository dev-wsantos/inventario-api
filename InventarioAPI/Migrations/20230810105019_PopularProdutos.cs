using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioAPI.Migrations
{
    public partial class PopularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PRODUTOS(Nome,Descricao,Preco,Situacao, ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
           "VALUES('Coca-Cola Diet','Refrigerante de Cola 350 ml',5.45, 0, 'cocacola.jpg',50, GETDATE(),1)");

            migrationBuilder.Sql("INSERT INTO PRODUTOS(Nome,Descricao,Preco,Situacao, ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                "VALUES('Lanche de Atum','Lanche de Atum com maionese',8.50, 1, 'atum.jpg',10, GETDATE(),2)");

            migrationBuilder.Sql("INSERT INTO PRODUTOS(Nome,Descricao,Preco,Situacao, ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
               "VALUES('Pudim 100 g','Pudim de leite condensado 100g', 6.75, 1, 'pudim.jpg',20, GETDATE(),3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PRODUTOS");
        }
    }
}
