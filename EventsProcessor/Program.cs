using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventsProcessor
{
    class Program
    {
        static void Main (string[] args)
        {
            var builder = new Builder.BuilderSettings ();
            builder.RunServices ().Wait();
            Console.ReadLine ();
        }
    }
}