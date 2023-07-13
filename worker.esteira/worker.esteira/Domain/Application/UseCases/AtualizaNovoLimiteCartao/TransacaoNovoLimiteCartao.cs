
using Domain.Core.Models.Entidades;
using Domain.Core.Models.Request;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{
    public record TransacaoNovoLimiteCartao
    {
        public string NumeroCartao { get; }
        public decimal Limite { get; set; }
        public decimal Renda { get; set; }
        public double FaixaCalculo { get; set; }
        public int Multiplicador { get; set; }
        public Cartao DadosCartao { get; set; }


        public TransacaoNovoLimiteCartao(EventoAtualizarNovoLimite evento)
        {
            this.NumeroCartao = evento.NumeroCartao;
            this.Limite = evento.Limite;
            this.Renda = evento.Renda;
            this.FaixaCalculo = evento.FaixaCalculo;
            this.Multiplicador = evento.Multiplicador;
        }


    }
}
