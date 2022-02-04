using System;
using System.Collections.Generic;
using System.Text;

namespace ProvaCode7.Shared
{
   public class StatusClienteListModelView
    {
        public byte IdStatus { get; set; }
        public string Descricao { get; set; }
        public bool IsContabilizaVenda { get; set; }
        public bool IsFinalizaCliente { get; set; }

    }
}
