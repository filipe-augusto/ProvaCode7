using System.Collections.Generic;

namespace ProvaCode7.Shared.Entities
{
    public class RetornoRequisicao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<string> Erros { get; set; }

    }
}
