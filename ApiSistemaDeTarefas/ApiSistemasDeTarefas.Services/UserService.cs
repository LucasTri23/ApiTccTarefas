using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemaDeTarefas.Repositories.Repositorios;

namespace ApiSistemasDeTarefas.Services
{
    public class UserService
    {
        private readonly UserRepositorio _repositorio;
        public UserService(UserRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public User Login(string email, string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                {
                    throw new ArgumentException("E-mail e senha são obrigatórios.");
                }

                _repositorio.AbrirConexao();
                User usuario = _repositorio.Login(email, senha);

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
