using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo necessario")]
        [MinLength(5,ErrorMessage ="Minimo 5 letras")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Campo necessario")]
        [MaxLength(10, ErrorMessage ="Maximo 10")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo necessario")]
        [MaxLength(15, ErrorMessage = "Maximo 15")]
        public string Telefone { get; set; }

        public decimal Credito { get; set; }

        public byte IdStatus { get; set; }

        public int IdEndereco { get; set; }

        public EnderecoViewModel Endereco { get; set; }

    }
}
