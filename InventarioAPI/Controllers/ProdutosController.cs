using InventarioAPI.Context;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly InventarioApiDbContext _context;

        public ProdutosController (InventarioApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get (int id)
        {
            try
            {
                var produto = _context.Produtos
               .Include(p => p.Categoria)
               .FirstOrDefault(p => p.Id == id);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado.");
                }

                // para remover a referência cíclica
                produto.Categoria.Produtos = null;

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpGet("Listar")]
        public ActionResult<IEnumerable<Produto>> ListarProdutos (string categoria, string descricao, bool? situacao)
        {
            try
            {
                var produtosQuery = _context.Produtos
                .Join(_context.Categorias,
                    p => p.CategoriaId,
                    c => c.Id,
                    (produto, categoria) => new
                    {
                        Produto = produto,
                        Categoria = categoria
                    });

                if (!string.IsNullOrEmpty(categoria))
                {
                    produtosQuery = produtosQuery.Where(p => p.Categoria.Nome.Contains(categoria));
                }

                if (!string.IsNullOrEmpty(descricao))
                {
                    produtosQuery = produtosQuery.Where(p => p.Produto.Nome.Contains(descricao) || p.Produto.Descricao.Contains(descricao));
                }

                if (situacao.HasValue)
                {
                    produtosQuery = produtosQuery.Where(p => p.Produto.Situacao == situacao.Value);
                }

                var produtos = produtosQuery
                    .Select(pf => new Produto
                    {
                        Id = pf.Produto.Id,
                        Nome = pf.Produto.Nome,
                        Descricao = pf.Produto.Descricao,
                        Preco = pf.Produto.Preco,
                        Situacao = pf.Produto.Situacao,
                        ImagemUrl = pf.Produto.ImagemUrl,
                        Estoque = pf.Produto.Estoque,
                        DataCadastro = pf.Produto.DataCadastro,
                        Categoria = pf.Categoria,
                    })
                    .AsNoTracking().ToList();

                if (produtos is null)
                {
                    return NotFound("Nenhum produto encontrado.");
                }

                return produtos;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpPost("Cadastrar")]
        public ActionResult Post (Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest();
                }

                if (produto.CategoriaId != 0)
                {
                    Categoria categoria = _context.Categorias.Find(produto.CategoriaId);

                    if (categoria is null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                                        "Não foi possível processar a sua solicitação. Categoria Inválida.");
                    }

                    produto.Categoria = categoria;

                }

                _context.Produtos?.Add(produto);
                _context.SaveChanges();

                // Evitando ciclos de referência
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(produto, options);

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, json);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }

        }

        [HttpPut("Alterar")]
        public ActionResult Put (int id, Produto produto)
        {

            try
            {
                if (id != produto.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                                        "Não foi possível processar a sua solicitação, verifique os dados enviados");
                }

                if (produto.CategoriaId != 0)
                {
                    Categoria categoria = _context.Categorias.Find(produto.CategoriaId);

                    if (categoria is null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, 
                                        "Não foi possível processar a sua solicitação, verifique os dados enviados");
                    }

                    produto.Categoria = categoria;
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(produto, options);

                return Ok(json);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete (int id)
        {
            try
            {
                var produto = _context.Produtos?.FirstOrDefault(p => p.Id == id);

                if (produto is null)
                {
                    return NotFound("Produto não localizado.");
                }

                _context.Produtos?.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }
    }
}
