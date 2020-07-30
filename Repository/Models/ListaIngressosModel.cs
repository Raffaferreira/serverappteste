using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Models
{
    public class ListaIngressosModel
    {
        public int CodigoIngresso { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public int Status { get; set; }
        public string Local { get; set; }
    }
}
