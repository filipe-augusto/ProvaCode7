using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
  public class RegistroAtendimentos : Base
    {
        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        public string NomeDaMaquinaOuIP { get; set; }

        public string Descricao { get; set; }
        public List<ProdutoOfertadoCliente> ProdutoOfertadoCliente { get; set; }

    }
}
