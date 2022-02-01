using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Models
{
    public class Produto : Base
    {
        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public int IdCategoria { get; set; }

        public virtual CategoriaProduto CategoriaProduto { get; set; }



    }
}
