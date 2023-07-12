
using Domain.Core.Models.Request;
using FluentValidation;

namespace Endpoints.Validators
{
    public class TransacaoBloquearCartaoValidator : AbstractValidator<BloquearCartaoRequest>
    {
        public TransacaoBloquearCartaoValidator()
        {

            #region Local 

            RuleFor(x => x.DadosCartao.NumeroCartao)
           .NotEmpty()
           .WithMessage("Numero do cartão não informado.");

            RuleFor(x => x.Motivo)
                .NotEmpty()
                .WithMessage("Motivo não informado.")
                .IsInEnum()
                .WithMessage("Motivo não registrado.");


            #endregion
        }



    }
}
