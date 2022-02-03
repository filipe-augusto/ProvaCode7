using System.Collections.Generic;

namespace ProvaCode7.Shared
{
    public class CategoriaProduto : Base
    {
        public int Descricao { get; set; }

        public virtual List<Produto>  Produtos { get; set; }
    }
}
