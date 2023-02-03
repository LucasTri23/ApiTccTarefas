using ApiSistamasDeTarefas.Domain.Exceptions;
using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemaDeTarefas.Repositories.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemasDeTarefas.Services
{
    public class EmpresaService
    {
        private readonly EmpresaRepositorio _repositorio;
        public EmpresaService(EmpresaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Empresa> Listar(string? cnpj)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ProcurarEmpresaPorCnpj(cnpj);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Empresa Obter(string cnpj)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(cnpj);
                return _repositorio.Obter(cnpj);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Empresa model)
        {
            try
            {
                ValidarModelEmpresa(model, true);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(string cpfCliente)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(cpfCliente);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Empresa model)
        {
            try
            {
                ValidarModelEmpresa(model);
                _repositorio.AbrirConexao();
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModelEmpresa(Empresa model, bool isUpdate = false)
        {
            if (model is null)
                throw new ValidacaoException("O json está mal formatado, ou foi enviado vazio.");

            if (string.IsNullOrWhiteSpace(model.RazaoSocial))
                throw new ValidacaoException("Razão Social é obrigatório.");

            if (model.RazaoSocial.Trim().Length < 3 || model.RazaoSocial.Trim().Length > 255)
                throw new ValidacaoException("O nome fantasia precisa ter entre 3 a 255 caracteres.");

            if (string.IsNullOrWhiteSpace(model.Cnpj))
                throw new ValidacaoException("O CNPJ é obrigatório.");

            if (!ValidarCnpj(model.Cnpj))
                throw new ValidacaoException("O CNPJ é inválido.");

            model.RazaoSocial = model.RazaoSocial.Trim();
            model.Cnpj = model.Cnpj.Trim();
        }

        private static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
