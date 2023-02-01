using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistamasDeTarefas.Domain.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataDeCadastro { get; set; }
    }
}
