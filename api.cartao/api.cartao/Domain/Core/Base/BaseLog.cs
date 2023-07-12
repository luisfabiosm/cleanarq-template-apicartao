namespace Domain.Core.Base
{
    public record BaseLog
    {
        public string Protocolo { get; set; }
        public DateTime DataOcorrencia { get; set; }
    }
}
