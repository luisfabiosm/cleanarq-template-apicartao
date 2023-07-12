using Domain.Core.Models.Request;
using FluentValidation;

namespace Endpoints.Validators
{
    public class TransacaoNovoLimiteCartaoValidator : AbstractValidator<NovoLimiteRequest>
    {
        public TransacaoNovoLimiteCartaoValidator()
        {

            #region Local 

            RuleFor(x => x.DadosCartao.NumeroCartao)
           .NotEmpty()
           .WithMessage("Numero cartão não informado.");


            #endregion
        }



    }
}
