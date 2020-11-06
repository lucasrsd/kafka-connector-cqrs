using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.WebApi.Models.Proposta
{
    public class PropostaRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public string Vendedor { get; set; }
        [Required]
        public string Produto { get; set; }
    }
}
