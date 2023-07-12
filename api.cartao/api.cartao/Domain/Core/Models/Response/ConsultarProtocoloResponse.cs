using Domain.Core.Enums;
using Domain.Core.Models.Entidades;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Core.Models.Response
{
    public struct ConsultarProtocoloResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Cartao? Cartao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public EnumMotivoBloqueio? MotivoBloqueio { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? InformacaoAdicoonal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? LimiteSolicitado { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Renda { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? FaixaCalculo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Multiplicador { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Conta? Conta { get; set; }

        public DateTime?  DataOcorrencia { get; set; }
        public string? Protocolo { get; set; }

        public ConsultarProtocoloResponse(LogBloqueioCartao log)
        {
            this.Cartao = log.Cartao;
            this.MotivoBloqueio = log.Motivo;
            this.InformacaoAdicoonal = log.InformacaoAdicoonal;
            this.LimiteSolicitado = null;
            this.Renda = null;
            this.FaixaCalculo = null;
            this.Multiplicador = null;
            this.Conta = null;
            this.DataOcorrencia = log.DataOcorrencia;
            this.Protocolo = log.Protocolo;
        }

        public ConsultarProtocoloResponse(LogPropostaLimiteCartao log)
        {
            this.Cartao = log.Cartao;
            this.MotivoBloqueio = null;
            this.InformacaoAdicoonal = null;
            this.LimiteSolicitado = log.Limite;
            this.Renda = log.Renda;
            this.FaixaCalculo = log.FaixaCalculo;
            this.Multiplicador = log.Multiplicador;
            this.Conta = null;
            this.DataOcorrencia = log.DataOcorrencia;
            this.Protocolo = log.Protocolo;
        }

        public ConsultarProtocoloResponse(LogPropostaSolicitaCartao log)
        {
            this.Cartao = null;
            this.MotivoBloqueio = null;
            this.InformacaoAdicoonal = null;
            this.LimiteSolicitado = null;
            this.Renda = null;
            this.FaixaCalculo = null;
            this.Multiplicador = null;
            this.Conta = log.Conta;
            this.DataOcorrencia = log.DataOcorrencia;
            this.Protocolo = log.Protocolo;
        }
    }
}
