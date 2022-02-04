using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
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

        public StatusCliente StatusCliente { get; set; }

        public int IdEndereco { get; set; }

        public Endereco Endereco { get; set; }

        public List<ProdutoOfertadoCliente> ProdutoOfertadoCliente { get; set; }
    }
}
