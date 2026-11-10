
using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaisController : ControllerBase
    {
        private readonly IMaterialRepository _repository;

        public MateriaisController(IMaterialRepository repository)
        {
            _repository = repository;
        }

        // GET - Listar todos os materiais
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var materiais = _repository.ObterTodos()
                .Select(m => new
                {
                    m.Id,
                    m.Titulo,
                    m.AnoPublicacao,
                    Tipo = m.GetType().Name,
                    Descricao = m.Descricao(),
                    PrazoEmprestimo = m.CalcularPrazo()
                });

            return Ok(materiais);
        }

        // GET - Buscar material pelo ID
        [HttpGet("{id:int}")]
        public IActionResult ObterPorId(int id)
        {
            var material = _repository.ObterPorId(id);

            if (material is null)
            {
                return NotFound("Material não encontrado.");
            }

            return Ok(material);
        }

        // POST - Cadastrar livro
        [HttpPost("livro")]
        public IActionResult AdicionarLivro([FromBody] Livro livro)
        {
            if (string.IsNullOrWhiteSpace(livro.Titulo))
                return BadRequest("O título é obrigatório.");

            if (livro.AnoPublicacao < 0)
                return BadRequest("O ano não pode ser negativo.");

            if (string.IsNullOrWhiteSpace(livro.Autor))
                return BadRequest("O autor é obrigatório.");

            if (livro.Paginas <= 0)
                return BadRequest("As páginas devem ser maiores que zero.");

            var novoLivro = _repository.Adicionar(livro);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = novoLivro.Id },
                novoLivro
            );
        }

        // POST - Cadastrar revista
        [HttpPost("revista")]
        public IActionResult AdicionarRevista([FromBody] Revista revista)
        {
            if (string.IsNullOrWhiteSpace(revista.Titulo))
                return BadRequest("O título é obrigatório.");

            if (revista.AnoPublicacao < 0)
                return BadRequest("O ano não pode ser negativo.");

            if (revista.Edicao <= 0)
                return BadRequest("A edição deve ser maior que zero.");

            if (revista.MesPublicacao < 1 || revista.MesPublicacao > 12)
                return BadRequest("O mês deve estar entre 1 e 12.");

            var novaRevista = _repository.Adicionar(revista);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = novaRevista.Id },
                novaRevista
            );
        }

        // PUT - Atualizar livro
        [HttpPut("livro/{id:int}")]
        public IActionResult AtualizarLivro(int id, [FromBody] Livro livro)
        {
            var material = _repository.ObterPorId(id);

            if (material is null)
                return NotFound("Material não encontrado.");

            if (material is not Livro)
                return BadRequest("O material não é um livro.");

            if (string.IsNullOrWhiteSpace(livro.Titulo))
                return BadRequest("O título é obrigatório.");

            if (livro.AnoPublicacao < 0)
                return BadRequest("O ano não pode ser negativo.");

            if (string.IsNullOrWhiteSpace(livro.Autor))
                return BadRequest("O autor é obrigatório.");

            if (livro.Paginas <= 0)
                return BadRequest("As páginas devem ser maiores que zero.");

            _repository.Atualizar(id, livro);

            return Ok(_repository.ObterPorId(id));
        }

        // PUT - Atualizar revista
        [HttpPut("revista/{id:int}")]
        public IActionResult AtualizarRevista(int id, [FromBody] Revista revista)
        {
            var material = _repository.ObterPorId(id);

            if (material is null)
                return NotFound("Material não encontrado.");

            if (material is not Revista)
                return BadRequest("O material não é uma revista.");

            if (string.IsNullOrWhiteSpace(revista.Titulo))
                return BadRequest("O título é obrigatório.");

            if (revista.AnoPublicacao < 0)
                return BadRequest("O ano não pode ser negativo.");

            if (revista.Edicao <= 0)
                return BadRequest("A edição deve ser maior que zero.");

            if (revista.MesPublicacao < 1 || revista.MesPublicacao > 12)
                return BadRequest("O mês deve estar entre 1 e 12.");

            _repository.Atualizar(id, revista);

            return Ok(_repository.ObterPorId(id));
        }

        // DELETE - Excluir material
        [HttpDelete("{id:int}")]
        public IActionResult Remover(int id)
        {
            var removido = _repository.Remover(id);

            if (!removido)
                return NotFound("Material não encontrado.");

            return NoContent();
        }

        // GET - Descrição e prazo de empréstimo
        [HttpGet("{id:int}/descricao")]
        public IActionResult ObterDescricao(int id)
        {
            var material = _repository.ObterPorId(id);

            if (material is null)
                return NotFound("Material não encontrado.");

            return Ok(new
            {
                material.Id,
                Tipo = material.GetType().Name,
                Descricao = material.Descricao(),
                PrazoEmprestimo = material.CalcularPrazo()
            });
        }

        // POST - Emprestar material
        [HttpPost("{id:int}/emprestar")]
        public IActionResult Emprestar(int id)
        {
            var material = _repository.ObterPorId(id);

            if (material is null)
                return NotFound("Material não encontrado.");

            if (material is not IEmprestimo emprestavel)
                return BadRequest("Este material não pode ser emprestado.");

            return Ok(emprestavel.Emprestar());
        }
    }
}
