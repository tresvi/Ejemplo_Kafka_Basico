using Confluent.Kafka;
using Newtonsoft.Json;

Console.WriteLine("Ejecutando Consumer simple...");

var config = new ConsumerConfig
{
    GroupId = "test-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();

consumer.Subscribe("test");

CancellationTokenSource token = new();

try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);
        if (response.Message != null)
        {
            Console.WriteLine($"Llego un msje!: {response.Message.Value}");
            //var weather = JsonConvert.DeserializeObject<Weather>(response.Message.Value);
            //Console.WriteLine($"State: {weather.State}, Temp: {weather.Temperature} ºC");
            Thread.Sleep(10);
        }
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    throw;
}
finally { consumer.Close(); }


public record Weather(string State, int Temperature);


