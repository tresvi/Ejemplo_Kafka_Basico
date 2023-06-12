using Confluent.Kafka;
using Newtonsoft.Json;

//FUENTE: https://www.youtube.com/watch?v=A4Y7z6wFRk0

var config = new ProducerConfig { 
    BootstrapServers = "localhost:9092", 
    Acks = Acks.Leader
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    string? mensaje;
    Console.WriteLine("Ingrese msjes para enviar a la cola:");
    while ((mensaje = Console.ReadLine()) != null)
    {
        var response = await producer.ProduceAsync("test",
            new Message<Null, string> { Value = mensaje});
    }
}
catch (ProduceException<Null, string> ex)
{
    Console.WriteLine(ex.Message);
}


public record Weather(string State, int Temperature)
{

}


/*Hacerlo todo en consolas separadas y dejarlas corriendo
 * 
 0)Instalacion: https://www.apache.org/dyn/closer.cgi?path=/kafka/2.1.0/kafka_2.11-2.1.0.tgz   (instalar el JDK de Java 8 tambien) 
 
1)Levantar coordinador de transaccion "Zookeeper" (en puerto 2181 por defecto):
PS C:\tools\kafka_2.11-2.1.0> .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties 

2) Levantar kafka (puerto 9092)
PS C:\tools\kafka_2.11-2.1.0> .\bin\windows\kafka-server-start.bat .\config\server.properties

3) Crear un topico
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic test

4)Verificar si se creó (se listan TODOS los topicos activos) 
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-topics.bat --list --zookeeper localhost:2181

5)Crear un producer. Aca ya se pueden ingresar lineas de texto al topico
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-console-producer.bat --broker-list localhost:9092 --topic test

6)Crear consumer (se recibiran los msjes enviados antes)
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic test --from-beginning


Desde Docker: Es recontra pesado:

1) curl --silent --output docker-compose.yml https://raw.githubusercontent.com/confluentinc/cp-all-in-one/6.1.1-post/cp-all-in-one/docker-compose.yml
2) docker-compose up -d

*/