
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
   public class ProdutoOfertadoCliente
    {
        
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        public int IdRegistroAtendimento { get; set; }
        public RegistroAtendimentos RegistroAtendimentos { get; set; }

        public DateTime DiaDaOferta { get; set; }
    }
}
