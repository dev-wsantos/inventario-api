# Inventario API



## Descrição do Projeto

Este repositório contém o código-fonte para uma Restful WebAPI ASP.NET Core que oferece funcionalidades de gerenciamento de categorias e produtos. O sistema permite realizar operações de cadastro, alteração, remoção e listagem de categorias e produtos, com opções de filtragem para facilitar a busca de informações específicas.

## Tecnologias Utilizadas

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core): Um framework de desenvolvimento web de alto desempenho para criar aplicativos modernos e escaláveis.
- [Entity Framework Core 6](https://docs.microsoft.com/pt-br/ef/core/what-is-new/ef-core-6.0): Um ORM (Object-Relational Mapping) que permite o acesso a bancos de dados de maneira simplificada e orientada a objetos.
- [C#](https://docs.microsoft.com/dotnet/csharp): Uma linguagem de programação moderna e orientada a objetos da plataforma .NET.
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads): Um sistema de gerenciamento de banco de dados relacional robusto e escalável.
- [Swagger](https://swagger.io/): Uma ferramenta para documentação e teste de APIs, facilitando a interação com a sua API.

## Requisitos do Sistema

- [.NET Core SDK](https://dotnet.microsoft.com/download) - Versão 6

- Ambiente de desenvolvimento de sua escolha (Visual Studio, Visual Studio Code, etc.).

- SQL Server 2019 ou superior

  

## Configuração

1. Faça o clone deste repositório para o seu ambiente local.

2. Abra o projeto em sua ferramenta de desenvolvimento escolhida.

3. Configure a conexão com o banco de dados de sua preferência no arquivo `appsettings.json`.

4. Execute as migrações para criar o esquema do banco de dados:

   ```powershell
   dotnet ef database update
   ```

5. Compile e execute o projeto.



## Endpoints da API

Aqui estão alguns exemplos de endpoints da API:

### `GET /api/Categorias/id`

Retorna uma categoria através de um identificador único.

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/2
```



### `GET /api/Categorias/Listar`

Retorna todos as categorias cadastradas. Aceita dois parâmetros onde podemos filtrar dados pelo nome (string) e pela situação da categoria: Ativo ou Inativo (boolean).

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/Listar?nome=Bebidas
```

```c#
https://localhost:7007/api/Categorias/Listar?situacao=true
```



### `POST /api/Categorias/Cadastrar`

Utilizando a interface do Swagger UI, clique no endpoint acima e localize o botão **Try it Out** e clique nele. Na área  **Request Body** verifique se a opção ***application/json*** está selecionada. 

Deve ser inserido um código JSON conforme modelo abaixo:



```json
{
  "nome": "string",
  "situacao": true, // pode ser true ou false
  "imagemUrl": "string" 
}
```



### `PUT /api/Categorias/Alterar`

Utilizando a interface do **Swagger UI**, clique no endpoint acima e localize o botão **Try it Ou**t e clique nele. Na área  **Request Body** verifique se a opção ***application/json*** está selecionada. 

Na área **Parameters**, insira o id da categoria na qual você deseja atualizar juntamente com os campos que deseja atualizar:

Deve ser inserido um código JSON conforme o modelo abaixo:

```json
 {
    "id": 2,
    "nome": "Fast Food",
    "situacao": true,
    "imagemUrl": "http://servidor.com/fastfood.jpg"
 }
```



### `DELETE /api/Categorias/id`

Exclui uma categoria através de um identificador único e em seguida retorna o item que foi excluído.

Exemplo de uso:

```c#
https://localhost:7007/api/Categorias/7


```

### `GET /api/Produtos/id`

Retorna uma categoria através de um identificador único.

Exemplo de uso:

```C#
https://localhost:7007/api/Produtos/3
```



### `GET /api/Produtos/Listar`

Retorna todos os produtos cadastradas. Aceita dois parâmetros onde podemos filtrar dados pelo nome da categoria (string), descrição do Produto (string) e pela situação da categoria:  **Ativo/Inativo** (boolean).

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

Utilizando a interface do **Swagger UI,** clique no endpoint acima e localize o botão Try it Out e clique nele. Na área  **Request Body** verifique se a opção ***application/json*** está selecionada. 

Deve ser inserido um código JSON conforme modelo abaixo:



```json
{

  "nome": "Hambúrger Artesanal",
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

Utilizando a interface do **Swagger UI,** clique no endpoint acima e localize o botão **Try it Out** e clique nele.  Na área **Parameters**, indique o **id** do produto que deseja alterar. Na área  **Request Body**, verifique se a opção ***application/json*** está selecionada. 



Deve ser inserido um código JSON conforme o modelo abaixo:



```json
{
  "id": 16,
  "nome": "Hambúrger Artesanal Suíno",
  "descricao": "Feito com carne suína",
  "preco": 30.25,
  "situacao": true,
  "imagemUrl": "http://servidor.com/hamburger-artesanal-suino.jpg",
  "estoque": 52,
  "dataCadastro": "2023-08-11T09:50:07.306Z",
  "categoriaId": 2
}
```



### `DELETE /api/Produtos/id`



Exclui um produto através de um identificador único e em seguida retorna o item que foi excluído.

Exemplo de uso:

```c#
https://localhost:7007/api/Produtos/16
```



## Autor

Wellington Santos (@dev-wsantos)

