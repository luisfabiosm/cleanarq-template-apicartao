using Domain.Core.Base;
using Domain.Core.Enums;
using Domain.Core.Models.Entidades;
using Domain.Core.Models.Request;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{
    public record TransacaoAdicionarNovoCartao 
    {
        internal string _numero;
        internal string _cvv;
        internal decimal _limite;

        public string Protocolo { get; set; }
        public Conta DadosConta { get; set; }
        public int DiaVencimento { get; set; }
        public EnumBandeiraCartao Bandeira { get; set; }
        public EnumTipoCartao TipoCartao { get; set; }
        public string NumeroCartao { get => _numero; }
        public string CVV { get => _cvv; }
        public decimal Limite { get => _limite; }


        public TransacaoAdicionarNovoCartao(EventoCriarNovoCartao evento)
        {
            this.Protocolo = evento.Protocolo;
            this.DadosConta = evento.DadosConta;
            this.DiaVencimento = evento.DiaVencimento;
            this.Bandeira = evento.Bandeira;
            this.TipoCartao = evento.TipoCartao;

            this._numero = gerarNumeroCartao();
            this._cvv = gerarCVV();
            this._limite = gerarLimiteInicial();

        }


        private string gerarNumeroCartao()
        {
            return "55" + new Random().Next(100000, 999999).ToString() + new Random().Next(1000, 9999).ToString();
        }

        private string gerarCVV()
        {
            return new Random().Next(100, 999).ToString();
        }

        private decimal gerarLimiteInicial()
        {
            return 500;
        }

    }
}
