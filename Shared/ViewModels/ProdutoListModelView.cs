using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
  public  class ProdutoListModelView
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool isSelect { get; set; }
    }
}
