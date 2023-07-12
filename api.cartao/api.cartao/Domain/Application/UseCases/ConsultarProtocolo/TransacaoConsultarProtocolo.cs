namespace Domain.Application.UseCases.ConsultarProtocolo
{
    public class TransacaoConsultarProtocolo
    {

        public string Protocolo { get; set; }

        public TransacaoConsultarProtocolo(string protocolo)
        {

            this.Protocolo = protocolo;
        }

    }
}
