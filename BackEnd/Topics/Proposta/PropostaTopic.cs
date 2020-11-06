using System.Threading.Tasks;
using BackEnd.WebApi.Models.Proposta;
using BackEnd.WebApi.Topics.Base;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BackEnd.WebApi.Topics.Proposta
{
    public class PropostaTopic : BaseTopic<PropostaRequest>, IPropostaTopic
    {
        public PropostaTopic(ClientConfig config, string name) : base(config, name) { }

        public void ProduzirNoTopico(PropostaRequest request)
        {
            base.Produce(request);
        }

        public Task CriarTopicoSeNaoExistir(int numPartitions, short replicationFactor)
        {
            return base.CreateTopicMaybe(numPartitions, replicationFactor);
        }
    }
}