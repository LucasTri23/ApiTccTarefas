using ApiSistamasDeTarefas.Domain.Exceptions;
using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemasDeTarefas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaDeTarefas.Controllers
{
    [Authorize]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;
        public TarefaController(TarefaService service)
        {
            _service = service;
        }
        [HttpGet("Listar Tarefas")]
        public IActionResult Listar(int id)
        {
            return StatusCode(200, _service.Listar(id));
        }

        [HttpGet("Buscar Tarefa por ID/{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            return StatusCode(200, _service.Listar(id));
        }

        [Authorize(Roles = "1")]
        [HttpPost("Adicionar Tarefa")]
        public IActionResult Adicionar([FromBody] Tarefa model)
        {
            try
            {
                _service.Inserir(model);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("Atualizar Tarefa")]
        public IActionResult Atualizar([FromBody] Tarefa model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("Deletar Tarefa/{id}")]
        public IActionResult Deletar([FromRoute] string nome)
        {
            _service.Deletar(nome);
            return StatusCode(200);
        }
    }
}
