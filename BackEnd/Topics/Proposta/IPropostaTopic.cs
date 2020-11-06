using System.Threading.Tasks;
using BackEnd.WebApi.Models.Proposta;
using BackEnd.WebApi.Topics.Base;
using Confluent.Kafka;

namespace BackEnd.WebApi.Topics.Proposta
{
    public interface IPropostaTopic
    {
        void ProduzirNoTopico(PropostaRequest request);
        Task CriarTopicoSeNaoExistir(int numPartitions, short replicationFactor);
    }
}