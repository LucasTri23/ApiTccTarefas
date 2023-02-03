using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistamasDeTarefas.Domain.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string NivelDeAcesso { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }
    }
}
