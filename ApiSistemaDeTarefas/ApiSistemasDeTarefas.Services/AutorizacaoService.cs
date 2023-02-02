using ApiSistamasDeTarefas.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemasDeTarefas.Services
{
    public class AutorizacaoService
    {
        private readonly IConfiguration _config;
        private readonly UsuarioService _usuarioService;
        public AutorizacaoService(UsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _config = configuration;
        }

        public Usuario Login(Usuario model)
        {
            var usuario = _usuarioService.ObterUsuarioPorCredenciais(model.Email, model.Senha);
            if (usuario is null)
                throw new InvalidOperationException("Usuário ou senha inválidos.");

            var senhaJwt = Encoding.ASCII.GetBytes
               (_config["SenhaJwt"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                       new Claim("Email", usuario.Email),
                       new Claim(ClaimTypes.Role, usuario.NivelDeAcesso.GetHashCode().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(senhaJwt),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return new Usuario()
            {
                Email = stringToken,
                Senha = usuario.NivelDeAcesso
            };
        }
    }
}
