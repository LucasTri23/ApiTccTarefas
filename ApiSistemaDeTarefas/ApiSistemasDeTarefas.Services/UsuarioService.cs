using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemaDeTarefas.Repositories.Repositorios;

namespace ApiSistemasDeTarefas.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepositorio _repositorio;
        public UsuarioService(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Usuario Login(string email, string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                {
                    throw new ArgumentException("E-mail e senha são obrigatórios.");
                }

                _repositorio.AbrirConexao();
                Usuario usuario = _repositorio.Login(email, senha);

                if (usuario == null)
                {
                    throw new InvalidOperationException("E-mail ou senha inválidos.");
                }

                return usuario;
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
    }
}
