using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
   public class APIVendasListModel
    {
        public string NomePruduto { get; set; }
        public string CodigoVenda { get; set; }
        public DateTime DiaVenda { get; set; }

        public decimal Valor { get; set; }
    }
}
