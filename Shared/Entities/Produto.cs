using System.Collections.Generic;

namespace ProvaCode7.Shared
{
    public class Produto : Base
    {
        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public int IdCategoria { get; set; }

        public virtual CategoriaProduto CategoriaProduto { get; set; }

        public List<ProdutoOfertadoCliente> ProdutoOfertadoCliente { get; set; }

    }
}
