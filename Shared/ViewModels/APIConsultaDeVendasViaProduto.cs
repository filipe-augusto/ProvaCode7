using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCallCenter.Shared
{
  public  class APIConsultaDeVendasViaProduto
    {
        public string NomeProduto { get; set; }

        public decimal ValorProduto { get; set; }

        public decimal ValorTotal { get; set; }

        public int QuantidadeDeVendas  { get; set; }
        public List<APIVendasListModel> Vendas { get; set; }

    }
}
