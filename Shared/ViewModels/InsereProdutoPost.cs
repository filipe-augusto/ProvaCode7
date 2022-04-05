using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
  public  class InsereProdutoPost
    {
        public string Token { get; set; }
        public decimal Valor { get; set; }
        public string NomeProduto { get; set; }

        public byte IdCategoria { get; set; }
    }
}
