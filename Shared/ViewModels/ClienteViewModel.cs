using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage ="Campo necessario")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo necessario")]
        public string Telefone { get; set; }

        public decimal Credito { get; set; }

        public byte IdStatus { get; set; }

        public int IdEndereco { get; set; }

        public EnderecoViewModel Endereco { get; set; }

    }
}
