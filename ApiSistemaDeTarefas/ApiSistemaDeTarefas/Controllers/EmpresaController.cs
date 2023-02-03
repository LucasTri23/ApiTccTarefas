using ApiSistamasDeTarefas.Domain.Exceptions;
using ApiSistemasDeTarefas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApiSistamasDeTarefas.Domain.Models;

namespace ApiSistemaDeTarefas.Controllers
{
    [Authorize]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _service;
        public EmpresaController(EmpresaService service)
        {
            _service = service;
        }

        [HttpGet("Buscar Empresa Por CNPJ/{cnpjEmpresa}")]
        public IActionResult Listar([FromQuery] string cnpj)
        {

            return StatusCode(200, _service.Listar(cnpj));
        }

        [HttpGet("Listar Empresas")]
        public IActionResult Obter([FromRoute] string? cnpj)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var login = identity.FindFirst("login").Value;
            return StatusCode(200, _service.Obter(cnpj));
        }

        [Authorize(Roles = "1")]
        [HttpPost("Inserir Empresa")]
        public IActionResult Inserir([FromBody] Empresa model)
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
        [HttpDelete("Deletar Empresa /{cnpjEmpresa}")]
        public IActionResult Deletar([FromRoute] string cnpj)
        {
            _service.Deletar(cnpj);
            return StatusCode(200);
        }

        [Authorize(Roles = "1")]
        [HttpPut("Atualizar Empresa")]
        public IActionResult Atualizar([FromBody] Empresa model)
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
