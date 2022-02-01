using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProvaCode7.Shared
{
    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "Campo necessario")]
        [MinLength(10, ErrorMessage = "Minimo 10 letras")]
        public string Cep { get; set; }
        [MinLength(50, ErrorMessage = "Minimo 50 letras")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Logradouro { get; set; }
   
        [Required(ErrorMessage = "Campo necessario")]
        [MinLength(50, ErrorMessage = "Minimo 50 letras")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        [MinLength(50, ErrorMessage = "Minimo 50 letras")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo necessario")]
        public string Estado { get; set; }

        public string Numero { get; set; }
        [MinLength(50, ErrorMessage = "Minimo 50 letras")]
        public string Complemento { get; set; }

    }
}
