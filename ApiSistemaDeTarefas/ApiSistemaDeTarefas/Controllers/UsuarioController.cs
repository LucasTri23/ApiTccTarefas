using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using ApiSistemasDeTarefas.Services;
using ApiSistamasDeTarefas.Domain.Models;

namespace ApiSistemasDeTarefas.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly AutorizacaoService _servico;

        public UsuarioController(UsuarioService usuarioService, UsuarioController service)
        {
            _usuarioService = usuarioService;
            _servico = service;
        }
        [HttpPost("Autorizacao")]
        public IActionResult Login(Usuario model)
        {
            try
            {
                var usuario = _usuarioService.ObterUsuarioPorCredenciais(model.Email, model.Senha);
                if (usuario == null)
                    return StatusCode(401);

                else
                    return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(401);
            }
        }
    }
}