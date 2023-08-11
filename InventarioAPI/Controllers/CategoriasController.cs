using InventarioAPI.Context;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly InventarioApiDbContext _context;
        public CategoriasController (InventarioApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get (int id)
        {
            try
            {
                var categoria = _context.Categorias
                .Include(p => p.Produtos)
                .FirstOrDefault(p => p.Id == id);

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrado.");
                }

                // Remover referência cíclica da lista de produtos
                foreach (var produto in categoria.Produtos)
                {
                    produto.Categoria = null;
                }


                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpGet("Listar")]
        public ActionResult<IEnumerable<Categoria>> ListarCategorias (string nome, bool? situacao)
        {
            try
            {
                var categoriasQuery = _context.Categorias
                .Include(c => c.Produtos)
                .AsQueryable();

                if (!string.IsNullOrEmpty(nome))
                {
                    categoriasQuery = categoriasQuery.Where(c => c.Nome.Contains(nome));
                }

                if (situacao.HasValue)
                {
                    categoriasQuery = categoriasQuery.Where(c => c.Situacao == situacao.Value);
                }

                var categoriasList = categoriasQuery.ToList();

                var categorias = categoriasList.Select(c => new Categoria
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Situacao = c.Situacao,
                    Produtos = c.Produtos.Select(p => new Produto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        Preco = p.Preco,
                        Situacao = p.Situacao,
                        ImagemUrl = p.ImagemUrl,
                        Estoque = p.Estoque,
                        DataCadastro = p.DataCadastro
                    }).ToList()
                }).ToList();

                if (categorias.Count == 0)
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }

                return categorias;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpPost("Cadastrar")]
        public ActionResult Post (Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest();
                }

                _context.Categorias?.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }

        [HttpPut("Alterar")]
        public ActionResult Put (int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                                       "Não foi possível processar a sua solicitação, verifique os dados enviados");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);
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
                var categoria = _context.Categorias?.FirstOrDefault(p => p.Id == id);

                if (categoria is null)
                {
                    return NotFound("Categoria não localizada.");
                }

                _context.Categorias?.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação.");
            }
        }
    }
}