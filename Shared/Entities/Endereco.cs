using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
{
    public class Endereco : Base
    {
        [Required]
        public string Cep { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
      
        public string Complemento { get; set; }

        public string Numero { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}
