using Domain.Core.Models.Request;
using FluentValidation;

namespace Endpoints.Validators
{
    public class TransacaoSolicitarCartaoValidator : AbstractValidator<SolicitarCartaoRequest>
    {
        public TransacaoSolicitarCartaoValidator()
        {

            #region Local 

            RuleFor(x => x.DadosConta.Agencia)
           .NotEmpty()
           .WithMessage("Agência da conta não informada.");

            RuleFor(x => x.DadosConta.Numero)
                .NotEmpty()
                .WithMessage("Numero da conta não informado.");

            #endregion
        }



    }
}
