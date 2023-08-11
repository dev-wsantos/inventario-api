# Inventario API



## Descri��o do Projeto

Este reposit�rio cont�m o c�digo-fonte para uma Restful WebAPI ASP.NET Core que oferece funcionalidades de gerenciamento de categorias e produtos. O sistema permite realizar opera��es de cadastro, altera��o, remo��o e listagem de categorias e produtos, com op��es de filtragem para facilitar a busca de informa��es espec�ficas.

## Tecnologias Utilizadas

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core): Um framework de desenvolvimento web de alto desempenho para criar aplicativos modernos e escal�veis.
- [Entity Framework Core 6](https://docs.microsoft.com/pt-br/ef/core/what-is-new/ef-core-6.0): Um ORM (Object-Relational Mapping) que permite o acesso a bancos de dados de maneira simplificada e orientada a objetos.
- [C#](https://docs.microsoft.com/dotnet/csharp): Uma linguagem de programa��o moderna e orientada a objetos da plataforma .NET.
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads): Um sistema de gerenciamento de banco de dados relacional robusto e escal�vel.
- [Swagger](https://swagger.io/): Uma ferramenta para documenta��o e teste de APIs, facilitando a intera��o com a sua API.

## Requisitos do Sistema

- [.NET Core SDK](https://dotnet.microsoft.com/download) - Vers�o 6

- Ambiente de desenvolvimento de sua escolha (Visual Studio, Visual Studio Code, etc.).

- SQL Server 2019 ou superior

  

## Configura��o

1. Fa�a o clone deste reposit�rio para o seu ambiente local.

2. Abra o projeto em sua ferramenta de desenvolvimento escolhida.

3. Configure a conex�o com o banco de dados de sua prefer�ncia no arquivo `appsettings.json`.

4. Execute as migra��es para criar o esquema do banco de dados:

   ```powershell
   dotnet ef database update
   ```

5. Compile e execute o projeto.



## Endpoints da API

Aqui est�o alguns exemplos de endpoints da API:

### `GET /api/Categorias/id`

Retorna uma categoria atrav�s de um identificador �nico.

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/2
```



### `GET /api/Categorias/Listar`

Retorna todos as categorias cadastradas. Aceita dois par�metros onde podemos filtrar dados pelo nome (string) e pela situa��o da categoria: Ativo ou Inativo (boolean).

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/Listar?nome=Bebidas
```

```c#
https://localhost:7007/api/Categorias/Listar?situacao=true
```



### `POST /api/Categorias/Cadastrar`

Utilizando a interface do Swagger UI, clique no endpoint acima e localize o bot�o **Try it Out** e clique nele. Na �rea  **Request Body** verifique se a op��o ***application/json*** est� selecionada. 

Deve ser inserido um c�digo JSON conforme modelo abaixo:



```json
{
  "nome": "string",
  "situacao": true, // pode ser true ou false
  "imagemUrl": "string" 
}
```



### `PUT /api/Categorias/Alterar`

Utilizando a interface do **Swagger UI**, clique no endpoint acima e localize o bot�o **Try it Ou**t e clique nele. Na �rea  **Request Body** verifique se a op��o ***application/json*** est� selecionada. 

Na �rea **Parameters**, insira o id da categoria na qual voc� deseja atualizar juntamente com os campos que deseja atualizar:

Deve ser inserido um c�digo JSON conforme o modelo abaixo:

```json
 {
    "id": 2,
    "nome": "Fast Food",
    "situacao": true,
    "imagemUrl": "http://servidor.com/fastfood.jpg"
 }
```



### `DELETE /api/Categorias/id`

Exclui uma categoria atrav�s de um identificador �nico e em seguida retorna o item que foi exclu�do.

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/7


```

### `GET /api/Produtos/id`

Retorna uma categoria atrav�s de um identificador �nico.

Exemplo de uso:

```C#
https://localhost:7007/api/Produtos/3
```



### `GET /api/Produtos/Listar`

Retorna todos os produtos cadastradas. Aceita dois par�metros onde podemos filtrar dados pelo nome da categoria (string), descri��o do Produto (string) e pela situa��o da categoria:  **Ativo/Inativo** (boolean).

Exemplos de uso:

```c#
https://localhost:7007/api/Produtos/Listar?categoria=Bebidas
```



```C#
https://localhost:7007/api/Produtos/Listar?descricao=lanche
```



```c#
https://localhost:7007/api/Produtos/Listar?situacao=true
```



### `POST /api/Produtos/Cadastrar`

Utilizando a interface do **Swagger UI,** clique no endpoint acima e localize o bot�o Try it Out e clique nele. Na �rea  **Request Body** verifique se a op��o ***application/json*** est� selecionada. 

Deve ser inserido um c�digo JSON conforme modelo abaixo:



```json
{

  "nome": "Hamb�rger Artesanal",
  "descricao": "Feito com carne bovina",
  "preco": 20.43,
  "situacao": true,
  "imagemUrl": "http://servidor.com/hamburger-artesanal.jpg",
  "estoque": 22,
  "dataCadastro": "2023-08-11T08:44:07.306Z",
  "categoriaId": 2
}
```



### `PUT /api/Produtos/Alterar`

Utilizando a interface do **Swagger UI,** clique no endpoint acima e localize o bot�o **Try it Out** e clique nele.  Na �rea **Parameters**, indique o **id** do produto que deseja alterar. Na �rea  **Request Body**, verifique se a op��o ***application/json*** est� selecionada. 



Deve ser inserido um c�digo JSON conforme o modelo abaixo:



```json
{
  "id": 16,
  "nome": "Hamb�rger Artesanal Su�no",
  "descricao": "Feito com carne su�na",
  "preco": 30.25,
  "situacao": true,
  "imagemUrl": "http://servidor.com/hamburger-artesanal-suino.jpg",
  "estoque": 52,
  "dataCadastro": "2023-08-11T09:50:07.306Z",
  "categoriaId": 2
}
```



### `DELETE /api/Produtos/id`



Exclui um produto atrav�s de um identificador �nico e em seguida retorna o item que foi exclu�do.

Exemplo de uso:

```c#
https://localhost:7007/api/Produtos/16
```



## Autor

Wellington Santos (@dev-wsantos)

