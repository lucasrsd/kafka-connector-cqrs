using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using EventsProcessor.ServiceReferences.PropostaApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EventsProcessor.Topics.Proposta
{
    public class PropostaTopic : IPropostaTopic
    {
        private bool Transacional = false;
        private readonly string name;
        private readonly ClientConfig config;
        private readonly IPropostaApi propostaApi;
        public PropostaTopic(ClientConfig config, string name, IPropostaApi propostaApi)
        {
            this.name = name;
            this.config = config;
            this.propostaApi = propostaApi;
        }

        public Task ConsumeTopic()
        {
            return new Task(() =>
           {
               Console.WriteLine($"{DateTime.Now} Iniciando consumo do topico: {this.name}");
               var consumerConfig = new ConsumerConfig(this.config);

               consumerConfig.GroupId = $"MeuGrupo";
               consumerConfig.AutoOffsetReset = AutoOffsetReset.Latest;

               if (Transacional)
               {
                   consumerConfig.EnableAutoCommit = false;
               }
               else
               {
                   consumerConfig.AutoCommitIntervalMs = 1000;
                   consumerConfig.EnableAutoCommit = true;
               }

               Console.WriteLine($"Iniciando consumidor....Grupo: {consumerConfig.GroupId} - Transacional: {Transacional} - EnableAutoCommit: {consumerConfig.EnableAutoCommit} - AutoCommitIntervalMs {consumerConfig.AutoCommitIntervalMs}");

               CancellationTokenSource cts = new CancellationTokenSource();
               Console.CancelKeyPress += (_, e) =>
               {
                   e.Cancel = true;
                   cts.Cancel();
               };

               using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
               {
                   consumer.Subscribe(this.name);
                   try
                   {
                       while (true)
                       {
                           var cr = consumer.Consume(cts.Token);
                           Console.WriteLine($"Processando fila: {this.name}, payload  {cr.Value} ");

                           var payload = JsonConvert.DeserializeObject<Root>(cr.Value);

                           this.propostaApi.ExecuteHttp(Method.PUT, new PropostaRequest(payload)).Wait();

                           if (Transacional)
                               consumer.Commit();
                       }
                   }
                   catch (OperationCanceledException e)
                   {
                       Console.WriteLine("Encerrando consumidor");
                   }
                   finally
                   {
                       consumer.Close();
                   }
               }
           });
        }
    }
}