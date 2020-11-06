using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Env;
using Newtonsoft.Json;
using RestSharp;

namespace FrontEnd.Data.Propostas
{
    public class PropostasServiceClient
    {
        public PropostasResult ObterPropostas()
        {
            var client = new RestClient($@"{BaseServiceRoute.BASE_ROUTE_SERVICE}/Proposta/Todas");
            var request = new RestRequest();

            request.Method = Method.GET;
            request.Parameters.Clear();

            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<MyArray>>(response.Content);

            return new PropostasResult()
            {
                Propostas = result.Take(100).ToList()
            };
        }
    }
}
