using System;
using System.Collections.Generic;

namespace FrontEnd.Data.Propostas
{
    public class PropostasResult
    {
      public List<MyArray> Propostas { get; set; } 
    }


    public class MyArray    {
        public string id { get; set; } 
        public int idProposta { get; set; } 
        public string cliente { get; set; } 
        public double valor { get; set; } 
        public string status { get; set; } 
        public string vendedor { get; set; } 
        public string produto { get; set; } 
        public DateTime? dtProposta { get; set; } 
        public DateTime dtAtualizacao { get; set; } 
    }
}
