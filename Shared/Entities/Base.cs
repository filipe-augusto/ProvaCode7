using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCallCenter.Shared
{
    public class Base
    {
        [Key]
        public int Id { get; set; }

        public bool IsAtivo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
