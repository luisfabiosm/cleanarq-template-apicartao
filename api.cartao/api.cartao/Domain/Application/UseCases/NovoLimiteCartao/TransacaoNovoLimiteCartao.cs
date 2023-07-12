using Domain.Core.Base;
using Domain.Core.Models.Entidades;

namespace Domain.Application.UseCases.NovoLimiteCartao
{
    public class TransacaoNovoLimiteCartao : BaseTransacao
    {
        public decimal Limite { get; }
        public decimal Renda { get; }
        public double FaixaCalculo { get; }
        public int Multiplicador { get; }
        public Cartao DadosCartao { get; }

        public TransacaoNovoLimiteCartao(Cartao cartao, decimal limite, decimal renda, int multiplicador, double faixa) : base(cartao.NumeroCartao)
        {
            base.setTransacaoProtocolo(cartao.CartaoConta.id);
            this.DadosCartao = cartao;
            this.Limite = limite;
            this.Renda = renda;
            this.FaixaCalculo = faixa;
            this.Multiplicador = multiplicador;
        }

    }
}
