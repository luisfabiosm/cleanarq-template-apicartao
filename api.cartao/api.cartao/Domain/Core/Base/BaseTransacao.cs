namespace Domain.Core.Base
{
    public abstract class BaseTransacao : BaseTransacaoProtocolo
    {
        public string NumeroCartao { get; }

        public BaseTransacao(string numero)
        {
            base.setTransacaoProtocolo(numero);
            this.NumeroCartao = numero;
        }
    }
}
