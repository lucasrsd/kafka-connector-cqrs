using System.Threading.Tasks;
using Confluent.Kafka;

namespace EventsProcessor.Topics.Proposta
{
    public class Root
    {
        public int id { get; set; }
        public string nm_cliente { get; set; }
        public double vl_proposta { get; set; }
        public string ds_status { get; set; }
        public string nm_vendedor { get; set; }
        public string nm_produto { get; set; }
        public long dt_proposta { get; set; }
        public long dt_atualizacao { get; set; }
    }
}