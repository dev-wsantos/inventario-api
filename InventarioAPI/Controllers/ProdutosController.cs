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

        [HttpGet("Listar")]
        public ActionResult<IEnumerable<Produto>> ListarProdutos (string categoria, string descricao, bool? situacao)
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
                .AsNoTracking() .ToList();

            if (produtos is null)
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return produtos;
        }




        [HttpPost("Cadastrar")]
        public ActionResult Post (Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }

            _context.Produtos?.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);

        }

        [HttpPut("Alterar")]
        public ActionResult Patch (int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete (int id)
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
    }
}
