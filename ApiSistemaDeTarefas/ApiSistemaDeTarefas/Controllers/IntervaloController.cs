using ApiSistamasDeTarefas.Domain.Exceptions;
using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemasDeTarefas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaDeTarefas.Controllers
{
    [Authorize]
    [ApiController]
    public class IntervaloController : ControllerBase
    {
        private readonly IntervaloService _service;
        public IntervaloController(IntervaloService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [HttpPost("Adicionar Intervalo")]
        public IActionResult Adicionar([FromBody] Intervalo model)
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

        [HttpPut("Atualizar Intervalo")]
        public IActionResult Atualizar([FromBody] Intervalo model)
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

    }
}
