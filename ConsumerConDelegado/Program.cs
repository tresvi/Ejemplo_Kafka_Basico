// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using System.Threading.Channels;

Console.WriteLine("Ejecutando Consumer con delegado...");


var conf = new ConsumerConfig
{
    GroupId = "my-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(conf)
    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
    .SetLogHandler((_, log) => Console.WriteLine($"Msje recibido: {log.Message}"))
    .Build())
{
    consumer.Subscribe("my-topic");

    while (true)
    { }

}