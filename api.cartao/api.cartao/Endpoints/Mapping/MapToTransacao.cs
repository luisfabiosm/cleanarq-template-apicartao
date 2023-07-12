using Domain.Application.UseCases.BloquearCartao;
using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Domain.Core.Models.Request;
using Endpoints.Validators;

namespace Endpoints.Mapping
{
    public static class MapToTransacao
    {

        public static TransacaoConsultarProtocolo ToTransacaoTransacaoConsultarProtocolo(string protocolo)
        {
            try
            {

                if (protocolo == "" || protocolo is null)
                {
                    throw new RequestHeaderException("Numero do Protocolo não informado.");
                }

            }
            catch (Exception ex)
            {
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoConsultarProtocolo(protocolo);

        }

        public static TransacaoConsultarCartao ToTransacaoTransacaoConsultarCartao(string numeroCartao)
        {
            try
            {

                if (numeroCartao == "" || numeroCartao is null)
                {
                    throw new RequestHeaderException("Numero do Cartão não informado.");
                }

            }
            catch (Exception ex)
            {
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoConsultarCartao(numeroCartao);

        }

        public static TransacaoConsultarBloqueioCartao ToTransacaoTransacaoConsultarBloqueioCartao(string numeroCartao)
        {
            try
            {

                if (numeroCartao == "" || numeroCartao is null)
                {
                    throw new RequestHeaderException("Numero do Cartão não informado.");
                }

            }
            catch (Exception ex)
            {
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoConsultarBloqueioCartao(numeroCartao);

        }

        public static TransacaoSolicitarCartao ToTransacaoTransacaoSolicitarCartao(HttpContext context, SolicitarCartaoRequest request)
        {
            try
            {
                new TransacaoSolicitarCartaoValidator().Validate(request);
            }
            catch (Exception ex)
            {
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoSolicitarCartao(request.DadosConta, request.DiaVencimento, (EnumBandeiraCartao)request.Bandeira, (EnumTipoCartao)request.TipoCartao);

        }

        public static TransacaoNovoLimiteCartao ToTransacaoTransacaoPropostaNovoLimiteCartao(HttpContext context, NovoLimiteRequest request)
        {
            try
            {
                new TransacaoNovoLimiteCartaoValidator().Validate(request);
            }
            catch (Exception ex)
            {
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoNovoLimiteCartao(request.DadosCartao, request.Limite, request.Renda, request.Multiplicador, request.FaixaCalculo);

        }

        public static TransacaoBloquearCartao ToTransacaoBloquearCartao(HttpContext context, BloquearCartaoRequest request)
        {
            try
            {
                new TransacaoBloquearCartaoValidator().Validate(request);
            }
            catch (Exception ex)
            { 
                throw new RequestHeaderException(ex.Message);
            }

            return new TransacaoBloquearCartao(request.DadosCartao, (EnumMotivoBloqueio)request.Motivo, request.InformacaoAdicoonal);

        }
    }
}
