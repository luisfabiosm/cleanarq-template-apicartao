namespace Domain.Core.Models.Entidades
{
    public record Conta
    {
        public string id { get; internal set; }
        public int Agencia { get; set; }
        public int Numero { get; set; }
        public Titular TitularConta { get; set; }
        public string Senha { get; set; }
    }
}
