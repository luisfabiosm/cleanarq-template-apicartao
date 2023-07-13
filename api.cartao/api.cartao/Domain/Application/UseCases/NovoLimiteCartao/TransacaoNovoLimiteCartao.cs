using Domain.Core.Base;
using Domain.Core.Models.Entidades;

namespace Domain.Application.UseCases.NovoLimiteCartao
{
    public class TransacaoNovoLimiteCartao : BaseTransacao
    {
        public decimal Limite { get; internal set; }
        public decimal Renda { get; internal set; }
        public double FaixaCalculo { get; internal set; }
        public int Multiplicador { get; internal set; }
        public Cartao DadosCartao { get; internal set; }

        public TransacaoNovoLimiteCartao(Cartao cartao, decimal limite, decimal renda, int multiplicador, double faixa) : base(cartao.NumeroCartao)
        {
            base.setTransacaoProtocolo(cartao.DadosConta.Numero.ToString());
            this.DadosCartao = cartao;
            this.Limite = limite;
            this.Renda = renda;
            this.FaixaCalculo = faixa;
            this.Multiplicador = multiplicador;
        }

    }
}
