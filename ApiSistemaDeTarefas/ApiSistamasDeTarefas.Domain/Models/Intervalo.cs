using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistamasDeTarefas.Domain.Models
{
    public class Intervalo
    {
        public int Id { get; set; }
        public string NomeDesenvolvedor { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }
        public int Duracao { get; set; }
    }
}
