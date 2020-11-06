using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using EventsProcessor.ServiceReferences.PropostaApi;
using EventsProcessor.Topics.Proposta;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventsProcessor.Builder
{
    public class BuilderSettings
    {
        private IConfiguration BuildSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public async Task RunServices()
        {
            Log("Compilando consumidores");
            var services = BuildServices();

            Log($"Iniciando consumidores, total: {services.Count}");
            services.ForEach(service => service.Start());

            Log($"Processamento em execução.");
            await Task.WhenAny(services);

            Log($"Alguma task foi encerrada, reiniciando processo.");
        }

        private List<Task> BuildServices()
        {
            var configuration = BuildSettings();

            var kafkaClientOptions = configuration
                .GetSection("KafkaClient")
                .Get<ClientConfig>();

            var servicesProvider = new ServiceCollection()
                  .AddSingleton<IConfiguration>(configuration).AddSingleton<IConfiguration>(configuration)
                  .AddSingleton<IPropostaApi>(serv => new PropostaApi(configuration.GetValue<string>("ServiceReferences:UrlProposta")))
                  .BuildServiceProvider();


            var processorProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IPropostaTopic>(serv => new PropostaTopic(kafkaClientOptions, "NOME_DO_TOPICO", servicesProvider.GetService<IPropostaApi> ()))
                .BuildServiceProvider();

            var tasksProcessors = new List<Task>();
            tasksProcessors.Add(processorProvider.GetService<IPropostaTopic>().ConsumeTopic());

            return tasksProcessors;
        }

        private void Log(string text)
        {
            Console.WriteLine($"{DateTime.Now} {text}");
        }
    }
}