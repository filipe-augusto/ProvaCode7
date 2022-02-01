using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Models
{
    public class Cliente  : Base
    {

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Telefone { get; set; }

        public decimal Credito { get; set; }

        public byte IdStatus { get; set; }

        public int IdEndereco { get; set; }

        public Endereco Endereco { get; set; }
    }
}
