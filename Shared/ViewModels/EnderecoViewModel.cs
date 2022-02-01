using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProvaCode7.Shared
{
    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "Campo necessario")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        public string Estado { get; set; }
        public string Complemento { get; set; }

    }
}
