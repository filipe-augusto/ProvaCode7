using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
   public class ClienteListModelView
    {

        public int Id { get; set; }

        public string IdClienteCifrado { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Status { get; set; }
        public byte IdStatus { get; set; }

    }
}
