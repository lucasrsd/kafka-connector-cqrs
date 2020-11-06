using System.Threading.Tasks;
using Confluent.Kafka;

namespace EventsProcessor.Topics.Proposta
{
    public interface IPropostaTopic
    {
        Task ConsumeTopic ();
    }
}