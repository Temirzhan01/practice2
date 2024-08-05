public class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>, IDisposable
{
    private readonly IProducer<TKey, TValue> _producer;

    public KafkaProducer(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            SecurityProtocol = SecurityProtocol.Plaintext, // Настроить в зависимости от конфигурации
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = configuration["Kafka:SaslUsername"],
            SaslPassword = configuration["Kafka:SaslPassword"]
        };

        _producer = new ProducerBuilder<TKey, TValue>(config)
            .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
            .SetLogHandler((_, logMessage) => Console.WriteLine($"Log: {logMessage.Message}"))
            .Build();
    }

    public async Task ProduceAsync(string topic, TKey key, TValue value)
    {
        try
        {
            var result = await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
            Console.WriteLine($"Message sent to {result.TopicPartitionOffset}");
        }
        catch (ProduceException<TKey, TValue> e)
        {
            Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
            // Реализация повторных попыток или логирования может быть добавлена здесь
        }
    }

    public void Flush(TimeSpan timeout)
    {
        _producer.Flush(timeout);
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
}
