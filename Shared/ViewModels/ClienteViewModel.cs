using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo necessario")]
   
        public string Nome { get; set; }
        [Required(ErrorMessage ="Campo necessario")]

        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo necessario")]

        public string Telefone { get; set; }

        public decimal Credito { get; set; }

        public byte IdStatus { get; set; }

        public string NomeStatus { get; set; }

        public int IdEndereco { get; set; }

        public bool isTelaOfertar { get; set; } = false;

        public EnderecoViewModel Endereco { get; set; }
        public List<ProdutoListModelView> ListProdutos { get; set; }

        public List<StatusClienteListModelView> ListaStatus { get; set; }

    }
}
