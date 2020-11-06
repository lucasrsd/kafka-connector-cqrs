using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd.WebApi.Topics.Base
{
    public class BaseTopic<T>
    {
        private readonly string name;
        private readonly int numPartitions;
        private readonly short replicationFactor;
        private readonly ClientConfig config;

        public BaseTopic (ClientConfig config, string name)
        {
            this.name = name;
            this.config = config;
        }

        public async Task CreateTopicMaybe (int numPartitions, short replicationFactor)
        {
            using (var adminClient = new AdminClientBuilder (this.config).Build ())
            {
                try
                {
                    await adminClient.CreateTopicsAsync (new List<TopicSpecification>
                    {
                        new TopicSpecification { Name = this.name, NumPartitions = this.numPartitions, ReplicationFactor = this.replicationFactor }
                    });
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        Console.WriteLine ($"ERRO {this.name}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine ("Este topico ja existe");
                    }
                }
            }
        }

        public void Produce (T payload)
        {
            using (var producer = new ProducerBuilder<string, string> (config).Build ())
            {
                var key = Guid.NewGuid ().ToString ();
                var val = JObject.FromObject (new { payload, date = DateTime.Now }).ToString (Formatting.None);

                Console.WriteLine ($"Criando nova mensagem para o topico: {key} {val}");

                producer.Produce (this.name, new Message<string, string> { Key = key, Value = val },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine ($"ERRO: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine ($"Msg enviada para topico: {deliveryReport.TopicPartitionOffset}");
                        }
                    });

                producer.Flush (TimeSpan.FromSeconds (10));

                Console.WriteLine ($"Mensagem produziado ao topico: {this.name}");
            }
        }
    }
}