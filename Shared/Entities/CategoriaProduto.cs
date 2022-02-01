using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Models
{
    public class CategoriaProduto : Base
    {
        public int Descricao { get; set; }

        public virtual List<Produto>  Produtos { get; set; }
    }
}
