using System;
using System.Threading.Tasks;
using EventsProcessor.ServiceReferences.Base;
using EventsProcessor.Topics.Proposta;
using RestSharp;

namespace EventsProcessor.ServiceReferences.PropostaApi
{
    public class PropostaApi : BaseHttpClient, IPropostaApi
    {
        private readonly string url;
        private const string route = "/EventoEmProposta";
        public PropostaApi(string url)
        {
            this.url = url + route;
        }

        public async Task ExecuteHttp(Method method, object content)
        {
            Console.WriteLine($"Postando na url: {this.url} o conteudo: {content}");
            var result = await base.Execute(this.url, method, content);
            if (!result.IsSuccessful)
                throw new System.Exception("Erro ao atualizar proposta...");
        }
    }

    // Pode ser gerada com NSwag
    public class PropostaRequest
    {

        public PropostaRequest(Root origem) // Ideal seria um AutoMapper
        {
            this.IdProposta = origem.id;
            this.Cliente = origem.nm_cliente;
            this.Valor = Convert.ToDecimal(origem.vl_proposta);
            this.Status = origem.ds_status;
            this.Vendedor = origem.nm_vendedor;
            this.Produto = origem.nm_produto;
            this.DtProposta = DateTimeOffset.FromUnixTimeMilliseconds(origem.dt_proposta).DateTime;
            this.DtAtualizacao = DateTimeOffset.FromUnixTimeMilliseconds(origem.dt_atualizacao).DateTime;
        }

        public int IdProposta { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public string Vendedor { get; set; }
        public string Produto { get; set; }
        public DateTime? DtProposta { get; set; }
        public DateTime? DtAtualizacao { get; set; }
    }
}