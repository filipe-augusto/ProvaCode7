using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
  public  class APIConsultaDeVendasViaCliente
    {
        public string NomeCliente { get; set; }

        public string CPFCliente { get; set; }

        public decimal ValorTotal { get; set; }

        public int QuantidadeDeVendas  { get; set; }
        public List<APIVendasListModel> Vendas { get; set; }

    }
}
