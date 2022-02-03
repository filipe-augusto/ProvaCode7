using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProvaCode7.Shared
{
    public class StatusCliente 
    {
        [Key]
        public byte IdStatus { get; set; }
        public string Descricao { get; set; }
        public bool IsContabilizaVenda { get; set; }
        public bool IsFinalizaCliente { get; set; }
        public virtual IList<Cliente> Clientes { get; set; }

    }
}
