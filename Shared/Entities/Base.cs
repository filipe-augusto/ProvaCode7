using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }

        public bool IsAtivo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
