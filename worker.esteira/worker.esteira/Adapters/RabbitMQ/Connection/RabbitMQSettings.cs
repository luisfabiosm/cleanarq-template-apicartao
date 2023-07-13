namespace Adapters.RabbitMQ.Connection
{
    public record RabbitMQSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Retry { get; set; }
        public int Delay { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
