using Domain.Core.Base;
using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Application.UseCases.SolicitarCartao
{
    public class TransacaoSolicitarCartao : BaseTransacaoProtocolo
    {
        internal string _numero;
        internal string _cvv;
        internal decimal _limite;

        public Conta DadosConta { get; set; }
        public int DiaVencimento { get; set; }
        public EnumBandeiraCartao Bandeira { get; set; }
        public EnumTipoCartao TipoCartao { get; set; }
        public string NumeroCartao { get => _numero; }
        public string CVV { get => _cvv; }
        public decimal Limite { get => _limite; }



        public TransacaoSolicitarCartao(Conta conta, int diavencimento, EnumBandeiraCartao bandeira, EnumTipoCartao tipo)
        {
            base.setTransacaoProtocolo(conta.Numero.ToString());
            this.DadosConta = conta;
            this.DiaVencimento = diavencimento;
            this.Bandeira = bandeira;
            this.TipoCartao = tipo;

        }


        public void AtualizaDadosCartao(string numero, string cvv, decimal limite)
        {
            this._cvv = cvv;
            this._numero = numero;
            this._limite = limite;

        }

    }
}
