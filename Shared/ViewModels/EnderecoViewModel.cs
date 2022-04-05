using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoCallCenter.Shared
{
    public class EnderecoViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Campo necessario")]

        public string Cep { get; set; }

        [Required(ErrorMessage = "Campo necessario")]
        public string Logradouro { get; set; }
   
        [Required(ErrorMessage = "Campo necessario")]
  
        public string Bairro { get; set; }
 
        public string Cidade { get; set; }
    
        public string Estado { get; set; }

        public string Numero { get; set; }
        [MaxLength(50, ErrorMessage = "Minimo 50 letras")]
        public string Complemento { get; set; }

    }
}
